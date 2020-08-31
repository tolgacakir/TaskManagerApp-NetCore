using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.WebUi.Models;

namespace TaskManagerApp.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskManager;
        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger, ITaskService taskService, IUserService userService)
        {
            _logger = logger;
            _taskManager = taskService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var user = _userService.Login("FirstUser", "11111111");
            var tasks = _taskManager.GetListWithType(user.Id)
            .OrderBy(t => t.StartingDate)
            .ToList();
            var model = new TasksViewModel
            {
                User = user,
                Tasks = tasks,
            };
            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
