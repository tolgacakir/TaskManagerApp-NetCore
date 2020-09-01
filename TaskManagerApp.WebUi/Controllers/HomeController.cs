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
using TaskManagerApp.Entities.Concrete;
using TaskManagerApp.WebUi.Models;

namespace TaskManagerApp.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskManager;
        private readonly IUserService _userManager;
        private readonly ITaskTypeService _taskTypeManager;
        public HomeController(ILogger<HomeController> logger, ITaskService taskService, IUserService userService, ITaskTypeService taskTypeManager)
        {
            _logger = logger;
            _taskManager = taskService;
            _userManager = userService;
            _taskTypeManager = taskTypeManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.Login("FirstUser", "11111111");
            var tasks = _taskManager.GetListWithType(user.Id)
            .OrderBy(t => t.StartingDate)
            .ToList();
            var model = new TaskListViewModel
            {
                User = user,
                Tasks = tasks,
            };
            return View(model);

        }

        public IActionResult Create()
        {
            var user = _userManager.Login("FirstUser", "11111111");
            var model = new CreateEditTaskViewModel
            {
                Task = new Entities.Concrete.Task(),
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = user.Id
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(Entities.Concrete.Task task)
        {
            var user = _userManager.Login("FirstUser", "11111111");
            var model = new CreateEditTaskViewModel
            {
                Task = task,
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = user.Id
            };
            try
            {
                _taskManager.Add(task);
                return RedirectToAction(nameof(Index));
            }
            catch(ValidationException ex)
            {
                TempData["Error!"]= ex.Message;
                return View(model);
            }
            catch (Exception)
            {
                TempData["Error!"] = "Unsuccessfully";
                return View(model);
            }
        }

        public IActionResult Update(int id)
        {
            var user = _userManager.Login("FirstUser", "11111111");
            var model = new CreateEditTaskViewModel
            {
                Task = _taskManager.GetListWithType(user.Id).FirstOrDefault(t => t.Id == id),
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = user.Id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Entities.Concrete.Task task)
        {
            var user = _userManager.Login("FirstUser", "11111111");
            var model = new CreateEditTaskViewModel
            {
                Task = task,
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = user.Id
            };
            try
            {
                _taskManager.Update(task);
                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException ex)
            {
                TempData["Error!"] = ex.Message;
                return View(model);
            }
            catch (Exception)
            {
                TempData["Error!"] = "Unsuccessfully";
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _taskManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
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
