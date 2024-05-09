using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult AddOrEditModal(int? empId = null)
        {
            if (!empId.HasValue)
                return PartialView(new User());

            var emp = context.User.FirstOrDefault(x => x.Id == empId.Value);
            if (emp == null)
                return NotFound();
            return PartialView(emp);
        }

        [HttpPost]
        public IActionResult Save(User model)
        {
            var user = context.User.FirstOrDefault(x => x.Id == model.Id);
            if (user == null)
            {
                user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Role = model.Role,
                    Birthday = model.Birthday
                };
                if (string.IsNullOrEmpty(model.Password))
                    user.Password = "123".HashMd5();
                else
                    user.Password = model.Password.HashMd5();
            }
            else
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Role = model.Role;
                user.Birthday = model.Birthday;
                if (!string.IsNullOrEmpty(model.Password))
                    user.Password = model.Password.HashMd5();
            }
            if (model.AvatarFile != null)
            {
                var path = "images/avts/" + model.AvatarFile.FileName;
                var uploadPath = Path.Combine("wwwroot", path);
                using(var stream = System.IO.File.Open(uploadPath, FileMode.OpenOrCreate)) { 
                    model.AvatarFile.CopyTo(stream);
                }
                user.Avatar = "/" + path;
            }
            if (model.Id == default)
                context.User.Add(user);
            else
                context.User.Update(user);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var emp = context.User.FirstOrDefault(x => x.Id == id);
            if (emp == null)
                return RedirectToAction(nameof(Index));

            context.User.Remove(emp);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
