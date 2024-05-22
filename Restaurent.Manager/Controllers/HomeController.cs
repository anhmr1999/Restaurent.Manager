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

        public IActionResult Index(int? choosed = null, DateTime? fromDate = null, DateTime? toDate = null, int type = 1)
        {
            if (User.IsInRole("Waiter"))
            {
                var tables = context.Table.Include(x => x.Bills).ToList();
                return View("Waiter", tables);
            }
            if (User.IsInRole("Chef"))
            {
                var chefBills = context.Bill.Include(x => x.Records).Include(x => x.Table).Where(x => x.CreatedAt.Date == DateTime.Now.Date || x.Records.Any(r => r.Status != 3)).ToList();
                if (choosed.HasValue)
                    ViewData["bill"] = chefBills.FirstOrDefault(x => x.Id == choosed);
                return View("Chef", chefBills);
            }

            var res = new AdminHomeModel();
            var bills = context.Bill.Where(x => x.CreatedAt.Date == DateTime.Now.Date).ToList();
            res.Revene = bills.Sum(x => x.Total);
            res.BillCount = bills.Count();
            if (fromDate.HasValue)
                res.FromDate = fromDate.Value;
            else
                res.FromDate = DateTime.Now.Date.AddDays(-4);
            if (toDate.HasValue)
                res.ToDate = toDate.Value;
            else
                res.ToDate = DateTime.Now.Date.AddDays(2);

            res.GroupType = type;
            var chartBill = context.Bill.Where(x => res.FromDate <= x.CreatedAt && x.CreatedAt <= res.ToDate).ToList();
            if (type == 1)
            {
                var allDate = new List<DateTime>();
                for (var i = res.FromDate; i <= res.ToDate; i = i.AddDays(1))
                    allDate.Add(i);
                res.ChartData = allDate.GroupJoin(chartBill, data => data.Date, bill => bill.CreatedAt.Date,
                    (d, bills) => new { key = d.Date, value = bills.Sum(x => x.Total) }).ToDictionary(x => x.key.ToString("dd-MM-yyyy"), x => x.value);
            }
            else if (type == 2)
            {
                var allMonth = new List<DateTime>();
                for (var i = new DateTime(res.FromDate.Year, res.FromDate.Month, 1); i <= res.ToDate; i = i.AddMonths(1))
                    allMonth.Add(i);
                res.ChartData = allMonth.GroupJoin(chartBill, data => new { data.Month, data.Year }, bill => new { bill.CreatedAt.Month, bill.CreatedAt.Year },
                    (m, bills) => new { key = m, value = bills.Sum(x => x.Total) }).ToDictionary(x => x.key.ToString("MM-yyyy"), x => x.value);
            }
            else
            {
                var allYear = new List<int>();
                for (int i = res.FromDate.Year; i <= res.ToDate.Year; i++)
                    allYear.Add(i);

                res.ChartData = allYear.GroupJoin(chartBill, data => data, bill => bill.CreatedAt.Year,
                    (y, bills) => new { key = y, value = bills.Sum(x => x.Total) }).ToDictionary(x => x.key.ToString(), x => x.value);
            }
            return View(res);
        }

        public IActionResult Menu(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var table = context.Table.FirstOrDefault(x => x.Id == id);
            ViewData["bill"] = new Bill() { TableId = id, Table = table, Records = new List<BillRecord>() };
            var foods = context.Food.ToList();
            return View(foods);
        }

        public IActionResult Bill(int id)
        {
            ViewData["bill"] = context.Bill.Include(x => x.Records).ThenInclude(x => x.Food).Include(x => x.Table).FirstOrDefault(x => x.Id == id);
            var foods = context.Food.ToList();
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

                try
                {
                    var notiContent = $"Has new bill <b>#{model.Id}!</b>";
                    var userIds = context.User.Where(x => x.Role == "Chef").Select(x => x.Id).ToList();
                    var url = Url.Action("Index", "Home", new { choosed = model.Id });
                    context.AddRange(userIds.Select(x => new Notification
                    {
                        UserId = x,
                        Content = notiContent,
                        Url = url,
                        CreatedAt = DateTime.Now
                    }));
                    context.SaveChanges();
                }
                catch { }

                return RedirectToAction("Bill", new { id = model.Id });
            }
            else
            {
                var bill = context.Bill.FirstOrDefault(x => x.Id == model.Id);
                var currentRecords = context.BillRecord.Where(x => x.BillId == bill.Id).ToList();
                context.RemoveRange(currentRecords);
                var records = new List<BillRecord>();
                foreach (var item in model.Records.GroupBy(x => x.FoodId).Where(x => x.Sum(r => r.Quantity) > 0))
                {
                    var same = currentRecords.FirstOrDefault(x => x.FoodId == item.Key);

                    var food = foods.FirstOrDefault(f => f.Id == item.Key);
                    var record = new BillRecord();
                    record.FoodId = food.Id;
                    record.BillId = bill.Id;
                    record.FoodName = food.Name;
                    record.Price = food.Price;
                    record.Quantity = item.Sum(x => x.Quantity);
                    record.Note = string.Join(", ", item.Select(x => x.Note));
                    if (same != null && same.Status != 1 && same.Quantity == record.Quantity)
                    {
                        record.Status = same.Status;
                    } else
                    {
                        record.Status = 1;
                    }    
                    records.Add(record);
                }
                bill.SubTotal = records.Sum(x => x.Quantity * x.Price);
                bill.Tax = bill.SubTotal / 10;
                bill.ServiceFee = bill.SubTotal / 20;
                bill.Total = bill.SubTotal + bill.Tax + bill.ServiceFee;
                context.Update(bill);
                context.AddRange(records);
                context.SaveChanges();

                try
                {
                    var notiContent = $"Bill <b>#{model.Id}</b> is changed!";
                    var userIds = context.User.Where(x => x.Role == "Chef").Select(x => x.Id).ToList();
                    var url = Url.Action("Index", "Home", new { choosed = model.Id });
                    context.AddRange(userIds.Select(x => new Notification
                    {
                        UserId = x,
                        Content = notiContent,
                        Url = url,
                        CreatedAt = DateTime.Now
                    }));
                    context.SaveChanges();
                }
                catch { }
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

        public IActionResult PaymentModal(int id, bool confirm = false)
        {
            var bill = context.Bill.Include(x => x.Table).Include(x => x.Records).ThenInclude(x => x.Food).FirstOrDefault(x => x.Id == id);
            if (bill == null)
                return NotFound();
            var url = Url.Action("Payment", "Bill", new { id });
            if (confirm)
                url = Url.Action("ConfirmPayment", "Bill", new { id });
            ViewData["Url"] = url;
            return PartialView(bill);
        }

        public IActionResult Read(int id)
        {
            var notify = context.Notification.FirstOrDefault(x => x.Id == id);
            if (notify == null)
                return RedirectToAction("Index");

            notify.IsRead = true;
            context.Update(notify);
            context.SaveChanges();
            if (notify.RequiredPayment.HasValue)
                TempData["BillId"] = notify.RequiredPayment;
            return Redirect(notify.Url);
        }
    }
}
