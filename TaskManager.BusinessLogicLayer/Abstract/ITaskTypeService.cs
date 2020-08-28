using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Abstract
{
    public interface ITaskTypeService
    {
        TaskType GetById(byte taskTypeId);
        List<TaskType> GetAll();
    }
}
