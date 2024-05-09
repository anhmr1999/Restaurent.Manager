using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TableController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index(string searchKey, bool? status, int page = 1)
        {
            var query = context.Table.AsNoTracking();
            if (!string.IsNullOrEmpty(searchKey))
                query = query.Where(x => x.Name.ToLower().Contains(searchKey.ToLower()));
            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            var total = query.Count();
            var data = query.OrderBy(x => x.Name).Skip((page - 1) * 10).Take(10).ToList();
            var model = new PagingModel<Table>
            {
                Datas = data,
                Page = page,
                TotalPage = total / 10 + (total % 10 > 0 ? 1 : 0)
            };
            ViewData["searchKey"] = searchKey;
            ViewData["status"] = status;
            return View(model);
        }

        public IActionResult AddOrEditModal(int? tableId = null)
        {
            if (tableId == null)
                return PartialView(new Table());

            var tbl = context.Table.FirstOrDefault(x => x.Id == tableId);
            if (tbl == null)
                return NotFound();
            
            return PartialView(tbl);
        }

        [HttpPost]
        public IActionResult Save(Table model)
        {
            var tbl = context.Table.FirstOrDefault(x => x.Id == model.Id);
            if (tbl != null)
            {
                tbl.Name = model.Name;
                tbl.Status = model.Status;
                context.Table.Update(tbl);
            }
            else
            {
                context.Table.Add(model);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
