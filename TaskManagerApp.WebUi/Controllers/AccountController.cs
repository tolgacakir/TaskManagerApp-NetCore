using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.WebUi.Models;

namespace TaskManagerApp.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userManager;

        public AccountController(IUserService userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            Entities.Concrete.User user;
            try
            {
                if (string.IsNullOrEmpty(loginViewModel.Username) || string.IsNullOrEmpty(loginViewModel.Password))
                {
                    user = null;
                }
                else
                {
                    user = _userManager.Login(loginViewModel.Username, loginViewModel.Password);
                }
                
                if (user != null)
                {
                    var identity = new ClaimsIdentity(
                        new[]
                        {
                        new Claim("userid", user.Id.ToString()),
                        new Claim("username", user.Username),
                        },
                        "cookie"
                    );
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync("SecurityScheme", claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
