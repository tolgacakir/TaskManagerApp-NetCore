using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.Entities.Concrete;
using TaskManagerApp.WebUi.Models;

namespace TaskManagerApp.WebUi.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskManager;
        private readonly IUserService _userManager;
        private readonly ITaskTypeService _taskTypeManager;
        public TaskController(ILogger<TaskController> logger, ITaskService taskService, IUserService userService, ITaskTypeService taskTypeService)
        {
            _logger = logger;
            _taskManager = taskService;
            _userManager = userService;
            _taskTypeManager = taskTypeService;
        }

        // GET: TaskController/Details/5
        public IActionResult Details(int id)
        {
            var user = _userManager.Login("FirstUser", "11111111");
            var task = _taskManager.GetListWithType(user.Id)
                .FirstOrDefault(t => t.Id == id);
            var model = new TaskDetailsViewModel
            {
                Task = task,
            };
            return View(model);
        }
    }
}
