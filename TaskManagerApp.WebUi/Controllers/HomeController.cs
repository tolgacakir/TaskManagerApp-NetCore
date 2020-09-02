using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.Entities.Concrete;
using TaskManagerApp.WebUi.Extensions;
using TaskManagerApp.WebUi.Models;

namespace TaskManagerApp.WebUi.Controllers
{
    [Authorize]
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
            var user = new User
            {
                Id = User.Identity.UserId(),
                Username = User.Identity.Username()
            };

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
            var model = new CreateEditTaskViewModel
            {
                Task = new Entities.Concrete.Task(),
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = User.Identity.UserId(),
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(Entities.Concrete.Task task)
        {
            var model = new CreateEditTaskViewModel
            {
                Task = task,
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = User.Identity.UserId(),
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
            int userId = User.Identity.UserId();
            var model = new CreateEditTaskViewModel
            {
                Task = _taskManager.GetListWithType(userId).FirstOrDefault(t => t.Id == id),
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = userId,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Entities.Concrete.Task task)
        {
            var model = new CreateEditTaskViewModel
            {
                Task = task,
                TaskTypes = _taskTypeManager.GetAll(),
                UserId = User.Identity.UserId()
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
