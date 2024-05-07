using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    public class BillController : Controller
    {
        AppDbContext context = new AppDbContext();

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
    }
}
