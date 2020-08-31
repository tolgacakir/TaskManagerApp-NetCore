using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.CrossCuttingConcerns.Validation;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Concrete
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

        public List<Task> GetListWithType(int userId)
        {
            var result = _taskDal
                .GetQueryable()
                .Where(t => t.UserId == userId)
                .Include(t => t.Type)
                .ToList();

            return result;
        }
    }
}
