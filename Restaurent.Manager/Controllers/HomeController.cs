using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index(int? choosed = null)
        {
            if (User.IsInRole("Waiter"))
            {
                var tables = context.Table.Include(x => x.Bills).ToList();
                return View("Waiter", tables);
            }
            if (User.IsInRole("Chef"))
            {
                var bills = context.Bill.Include(x => x.Records).Include(x => x.Table).Where(x => x.CreatedAt.Date == DateTime.Now.Date || x.Records.Any(r => r.Status != 3)).ToList();
                if (choosed.HasValue)
                    ViewData["bill"] = bills.FirstOrDefault(x => x.Id == choosed);
                return View("Chef", bills);
            }    

            return View();
        }

        public IActionResult Menu(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var table = context.Table.FirstOrDefault(x => x.Id == id);
            ViewData["bill"] = new Bill() { TableId = id, Table = table, Records = new List<BillRecord>() };
            var foods = context.Food.Where(x => x.Status).ToList();
            return View(foods);
        }

        public IActionResult Bill(int id)
        {
            ViewData["bill"] = context.Bill.Include(x => x.Records).ThenInclude(x => x.Food).Include(x => x.Table).FirstOrDefault(x => x.Id == id);
            var foods = context.Food.Where(x => x.Status).ToList();
            return View("Menu", foods);
        }

        [HttpPost]
        public IActionResult SaveBill(Bill model)
        {
            var foodIds = model.Records.Select(x => x.FoodId).ToArray();
            var foods = context.Food.Where(x => foodIds.Contains(x.Id)).ToList();
            if (model.Id == default)
            {
                model.CreatedAt = DateTime.Now;
                try
                {
                    model.Records = model.Records.Select(x =>
                    {
                        var food = foods.FirstOrDefault(f => f.Id == x.FoodId);
                        if (food == null)
                            throw new Exception();
                        x.FoodName = food.Name;
                        x.Price = food.Price;
                        x.Status = 1;
                        return x;
                    }).ToList();
                    model.SubTotal = model.Records.Sum(x => x.Quantity * x.Price);
                    model.Tax = model.SubTotal / 10;
                    model.ServiceFee = model.SubTotal / 20;
                    model.Total = model.SubTotal + model.Tax + model.ServiceFee;
                    context.Add(model);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Bill", new { id = model.Id });
            }
            else
            {
                var bill = context.Bill.FirstOrDefault(x => x.Id == model.Id);
                var currentRecords = context.BillRecord.Where(x => x.BillId == bill.Id).ToList();
                context.RemoveRange(currentRecords);
                var records = new List<BillRecord>();
                foreach (var item in model.Records.GroupBy(x => x.FoodId))
                {
                    var food = foods.FirstOrDefault(f => f.Id == item.Key);
                    var record = new BillRecord();
                    record.FoodId = food.Id;
                    record.BillId = bill.Id;
                    record.FoodName = food.Name;
                    record.Price = food.Price;
                    record.Quantity = item.Sum(x => x.Quantity);
                    record.Note = string.Join(", ", item.Select(x => x.Note));
                    record.Status = 1;
                    records.Add(record);
                }
                bill.SubTotal = records.Sum(x => x.Quantity * x.Price);
                bill.Tax = bill.SubTotal / 10;
                bill.ServiceFee = bill.SubTotal / 20;
                bill.Total = bill.SubTotal + bill.Tax + bill.ServiceFee;
                context.Update(bill);
                context.AddRange(records);
                context.SaveChanges();
                return RedirectToAction("Bill", new { id = bill.Id });
            }
        }

        public IActionResult AddModal(int id)
        {
            var food = context.Food.FirstOrDefault(x => x.Id == id);
            if (food == null)
                return NotFound();
            return PartialView(food);
        }

        public IActionResult PaymentModal(int id)
        {
            var bill = context.Bill.Include(x => x.Table).Include(x => x.Records).ThenInclude(x => x.Food).FirstOrDefault(x => x.Id == id);
            if(bill == null)
                return NotFound();
            return PartialView(bill);
        }
    }
}
