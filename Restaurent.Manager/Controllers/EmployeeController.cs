using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index(string searchKey, string role, int page = 1)
        {
            var query = context.User.AsNoTracking();
            if (!string.IsNullOrEmpty(searchKey))
                query = query.Where(x => x.Name.ToLower().Contains(searchKey.ToLower()) || x.Email.ToLower().Contains(searchKey.ToLower()));
            if (!string.IsNullOrEmpty(role))
                query = query.Where(x => x.Role == role);

            var total = query.Count();
            var data = query.OrderBy(x => x.Role).Skip((page - 1) * 10).Take(10).ToList();
            var model = new PagingModel<User>
            {
                Datas = data,
                Page = page,
                TotalPage = total / 10 + (total % 10 > 0 ? 1 : 0)
            };
            ViewData["searchKey"] = searchKey;
            ViewData["role"] = role;
            return View(model);
        }

        public IActionResult AddModal()
        {
            return PartialView();
        }
    }
}
