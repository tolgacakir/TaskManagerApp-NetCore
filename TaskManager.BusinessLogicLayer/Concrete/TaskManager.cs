using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.Aspects.Castle.Caching;
using TaskManagerApp.Core.Aspects.Castle.Transaction;
using TaskManagerApp.Core.Aspects.Castle.Validation;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
        }

        [ValidationAspect(typeof(TaskValidator))]
        public void Add(Task task)
        {
            _taskDal.Add(task);
        }

        [ValidationAspect(typeof(TaskValidator))]
        public void Update(Task task)
        {
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

        [CacheAspect(1)]
        public List<Task> GetListWithType(int userId)
        {
            var result = _taskDal
                .GetQueryable()
                .Where(t => t.UserId == userId)
                .Include(t => t.Type)
                .ToList();
            return result;
        }

        [TransactionScopeAspect]
        public void TransactionalOperationExample(Task task)
        {
            _taskDal.Update(task);
            _taskDal.Add(task);
        }
    }
}
