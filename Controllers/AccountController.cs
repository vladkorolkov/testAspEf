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
       
        private readonly UsersContext _db;

        public AccountController (UsersContext db)
        {
            _db = db;
        }
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
                
                    User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == registerModel.Email);
                    if (user == null)
                    {
                        _db.Users.Add(new Models.User 
                        {
                            Email = registerModel.Email,
                            Password = registerModel.Password
                        });
                        await _db.SaveChangesAsync();
                        await Authenticate(registerModel.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    else 
                        ModelState.AddModelError("", "Некорректный email или пароль");                    
                               
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            
                if (ModelState.IsValid)
                    {   
                        
                        User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                            if (user != null)
                                {
                                    await Authenticate(model.Email); // аутентификация
 
                                    return RedirectToAction("Index", "Home");
                                }
                            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
            return View(model);
            
        
        }
        
    }
}