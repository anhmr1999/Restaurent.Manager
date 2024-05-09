using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurent.Manager.Models;
using System.Security.Claims;
using System.Text;

namespace Restaurent.Manager.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index(string ReturnUrl = null)
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model, string ReturnUrl = null)
        {
            var context = new AppDbContext();
            var pwd = model.Password.HashMd5();
            var user = context.User.FirstOrDefault(x => x.Email == model.Email && x.Password == pwd);
            if (user == null)
            {
                ViewData["Error"] = "Email or password is not correct";
                return View(model);
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            if(!string.IsNullOrEmpty(ReturnUrl))
                return Redirect(ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
