using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    public class DishController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index(string searchKey, int? type, int page = 1)
        {
            var query = context.Food.AsNoTracking();
            if (!string.IsNullOrEmpty(searchKey))
                query = query.Where(x => x.Name.ToLower().Contains(searchKey.ToLower()));
            if (type.HasValue)
                query = query.Where(x => x.TypeId == type);
            var total = query.Count();
            var data = query.OrderBy(x => x.Name).Skip((page - 1) * 10).Take(10).ToList();
            var model = new PagingModel<Food>
            {
                Datas = data,
                Page = page,
                TotalPage = total / 10 + (total % 10 > 0 ? 1 : 0)
            };
            ViewData["searchKey"] = searchKey;
            ViewData["type"] = type;
            return View(model);
        }

        public IActionResult AddModal()
        {
            return PartialView();
        }
    }
}
