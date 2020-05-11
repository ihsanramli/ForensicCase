using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ForensicCase.Models;
using ForensicCase.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ForensicCase.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration Configuration;

        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                ForensicRepository repo = new ForensicRepository(Configuration);

                var result = repo.GetSingleUser(loginViewModel.UserName);

                if(String.IsNullOrEmpty(result.username))
                {
                    ModelState.AddModelError(string.Empty,
                        "Unable to login. The username inserted is not valid.");
                    return View();
                }

                var claims = new[] { new Claim(ClaimTypes.Name, result.username) };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Reset()
        {
            ForensicRepository repo = new ForensicRepository(Configuration);

            repo.ResetDb();

            HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}