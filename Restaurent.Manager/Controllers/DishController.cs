using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult AddOrEditModal(int? dishId = null)
        {
            if (dishId == null)
                return PartialView(new Food());

            var dish = context.Food.FirstOrDefault(x => x.Id == dishId);
            if (dish == null)
                return NotFound();
            return PartialView(dish);
        }

        [HttpPost]
        public IActionResult Save(Food model)
        {
            var dish = context.Food.FirstOrDefault(x => x.Id == model.Id);
            if (dish == null)
            {
                dish = new Food
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Status = model.Status,
                    TypeId = model.TypeId
                };
            }
            else
            {
                dish.Name = model.Name;
                dish.Price = model.Price;
                dish.Description = model.Description;
                dish.Status = model.Status;
                dish.TypeId = model.TypeId;
                context.Food.Update(dish);
            }
            if (model.ImageFile != null)
            {
                var path = "images/" + model.ImageFile.FileName;
                var uploadPath = Path.Combine("wwwroot", path);
                using (var stream = System.IO.File.Open(uploadPath, FileMode.OpenOrCreate))
                {
                    model.ImageFile.CopyTo(stream);
                }
                dish.Image = "/" + path;
            }
            if (model.Id == default)
                context.Food.Add(dish);
            else
                context.Food.Update(dish);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var dish = context.Food.FirstOrDefault(x => x.Id == id);
            if (dish == null)
                return RedirectToAction(nameof(Index));

            context.Food.Remove(dish);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
