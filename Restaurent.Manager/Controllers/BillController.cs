using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    public class BillController : Controller
    {
        AppDbContext context = new AppDbContext();

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchKey, bool? status, DateTime? createdDate, int page = 1)
        {
            var query = context.Bill.Include(x => x.Table).AsNoTracking();
            if (!string.IsNullOrEmpty(searchKey))
                query = query.Where(x => x.Id.ToString().Contains(searchKey) || x.Table.Name.Contains(searchKey));
            if (status.HasValue)
                query = query.Where(x => x.PaidAt.HasValue == status);
            if (createdDate.HasValue)
                query = query.Where(x => x.CreatedAt.Date == createdDate.Value.Date);

            var total = query.Count();
            var data = query.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * 10).Take(10).ToList();
            var model = new PagingModel<Bill>
            {
                Datas = data,
                Page = page,
                TotalPage = total / 10 + (total % 10 > 0 ? 1 : 0)
            };
            ViewData["searchKey"] = searchKey;
            ViewData["status"] = status;
            ViewData["createdDate"] = createdDate;
            return View(model);
        }

        [Authorize(Roles = "Chef")]
        public IActionResult Progress(int billId, int foodId)
        {
            var record = context.BillRecord.FirstOrDefault(x => x.BillId == billId && x.FoodId == foodId);
            if (record == null)
                return NotFound();
            if (record.Status != 1)
                return BadRequest();
            record.Status = 2;
            context.Update(record);
            context.SaveChanges();
            return RedirectToAction("Index", "Home", new { choosed = billId });
        }

        [Authorize(Roles = "Chef")]
        public IActionResult Done(int billId, int foodId)
        {
            var record = context.BillRecord.FirstOrDefault(x => x.BillId == billId && x.FoodId == foodId);
            if (record == null)
                return NotFound();
            if (record.Status != 2)
                return BadRequest();
            record.Status = 3;
            context.Update(record);
            context.SaveChanges();
                var bill = context.Bill.Include(x => x.Table).Include(x => x.Records).FirstOrDefault(x => x.Id == billId);
            try
            {
                var notiContent = $"Table <b>{bill.Table.Name} - #{billId}</b>, <b>#{record.FoodName}</b> is done!";
                var userIds = context.User.Where(x => x.Role == "Waiter").Select(x => x.Id).ToList();
                var url = Url.Action("Bill", "Home", new { id = billId });
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
            if (bill.Records.Any(x => x.Status != 3))
                return RedirectToAction("Index", "Home", new { choosed = billId });
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Waiter")]
        public IActionResult Payment(int id) {
            var bill = context.Bill.Include(x => x.Records).FirstOrDefault(x => x.Id == id);
            if (bill == null || bill.PaidAt.HasValue)
                return RedirectToAction("Index", "Home");
            if (bill.Records.Any(x => x.Status != 3))
                return RedirectToAction("Bill", "Home", new { id });

            var notiContent = $"Bill <b>#{bill.Id}</b> requires payment!";
            var userIds = context.User.Where(x => x.Role == "Admin").Select(x => x.Id).ToList();
            var url = Url.Action("Index", "Bill");
            context.AddRange(userIds.Select(x => new Notification
            {
                UserId = x,
                Content = notiContent,
                Url = url,
                RequiredPayment = id,
                CreatedAt = DateTime.Now
            }));
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmPayment(int id)
        {
            var bill = context.Bill.Include(x => x.Records).FirstOrDefault(x => x.Id == id);
            if (bill == null || bill.PaidAt.HasValue)
                return RedirectToAction("Index", "Bill");
            if (bill.Records.Any(x => x.Status != 3))
                return RedirectToAction("Bill", "Home", new { id });

            bill.PaidAt = DateTime.Now;
            context.Update(bill);
            context.SaveChanges();
            TempData["BillId"] = id;
            return RedirectToAction("Index", "Bill");
        }
    }
}
