using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.WebUi.Models;

namespace TaskManagerApp.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userManager;

        public AccountController(ILogger<AccountController> logger, IUserService userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            Entities.Concrete.User user = null;
            try
            {
                loginViewModel.Username ??= "";
                loginViewModel.Password ??= "";
                var tempUser = new Entities.Concrete.User
                {
                    Username = loginViewModel.Username,
                    Password = loginViewModel.Password
                };
                user = _userManager.Login(tempUser);
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
                    _logger.LogInformation("Logged in. Username:" + user.Username + " DateTime:" + DateTime.Now);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Username or password is invalid.");
                    return View();
                }
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Error();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}
