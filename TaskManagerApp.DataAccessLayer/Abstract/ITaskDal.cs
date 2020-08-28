using System;
using System.Linq;
using System.Linq.Expressions;
using TaskManagerApp.Core.DataAccess;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Abstract
{
    public interface ITaskDal : IEntityRepository<Task>
    {
        Task GetWithTypeById(int taskId);
        IQueryable<Task> GetQueryableWithType(Expression<Func<Task, bool>> filter = null);
    }
}
