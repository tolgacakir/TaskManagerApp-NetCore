using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.BusinessLogicLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManager.BusinessLogicLayer.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly ITaskDal _taskDal;

        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
        }

        public void Add(Task task)
        {
            _taskDal.Add(task);
        }

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
    }
}
