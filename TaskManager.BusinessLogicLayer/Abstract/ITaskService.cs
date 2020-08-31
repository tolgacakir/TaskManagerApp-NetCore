using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Abstract
{
    public interface ITaskService
    {
        List<Task> GetAll(int userId);
        List<Task> GetListWithType(int userId);

        //List<Task> GetListAreActiveNotCompleted(int userId);
        
        //List<Task> GetListAreNotStarted(int userId);
        
        //List<Task> GetListAreOutOfDate(int userId);

        void Add(Task task);
        void Update(Task task);
        void Delete(int taskId);
    }
}
