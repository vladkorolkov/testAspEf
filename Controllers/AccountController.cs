using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using southSoundWebsite.Models;
using southSoundWebsite.ViewModels;

namespace southSoundWebsite.Controllers
{
    public class AccountController : Controller
    {
       

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                using (UsersContext db = new UsersContext())
                {
                    User user = await db.Users.FirstOrDefaultAsync(u => u.Email == registerModel.Email);
                    if (user == null)
                    {
                        db.Users.Add(new Models.User 
                        {
                            Email = registerModel.Email,
                            Password = registerModel.Password
                        });
                        await db.SaveChangesAsync();
                        await Authenticate(registerModel.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    else 
                        ModelState.AddModelError("", "Некорректный email или пароль");                    
                }                
            }
            return View(registerModel);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity
            (claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}