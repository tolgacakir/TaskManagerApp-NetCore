using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.BusinessLogicLayer.Abstract;
using TaskManager.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.CrossCuttingConcerns;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManager.BusinessLogicLayer.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly IValidator _validator;
        private readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
            _validator = new TaskValidator();
        }

        public void Add(Task task)
        {
            ValidatorTool.Validate(_validator, task);
            _taskDal.Add(task);
        }

        public void Update(Task task)
        {
            ValidatorTool.Validate(_validator, task);
            _taskDal.Update(task);
        }

        public void Delete(int taskId)
        {
            var task = _taskDal.Get(t => t.Id == taskId);
            _taskDal.Delete(task);
        }

        public List<Task> GetAll(int userId)
        {
            return _taskDal.GetList(t => t.UserId == userId);
        }
    }
}
