using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Concrete
{
    public class TaskTypeManager : ITaskTypeService
    {
        private readonly ITaskTypeDal _taskTypeDal;

        public TaskTypeManager(ITaskTypeDal taskTypeDal)
        {
            _taskTypeDal = taskTypeDal;
        }

        public List<TaskType> GetAll()
        {
            return _taskTypeDal.GetList();
        }

        public TaskType GetById(byte taskTypeId)
        {
            return _taskTypeDal.Get(t => t.Id == taskTypeId);
        }
    }
}
