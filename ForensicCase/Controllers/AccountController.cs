using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ForensicCase.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "Name") };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            SignIn(new ClaimsPrincipal(identity), CookieAuthenticationDefaults.AuthenticationScheme);
            
            return View();
        }

        public IActionResult Logout()
        {
            SignOut();
            return RedirectToAction("Login");
        }
    }
}