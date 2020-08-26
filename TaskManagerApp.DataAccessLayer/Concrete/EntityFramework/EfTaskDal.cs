using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskManagerApp.Core.DataAccess.EntityFramework;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework
{
    public class EfTaskDal : EfEntityRepositoryBase<Task,TaskManagerDbContext>, ITaskDal
    {
        public Task GetWithTypeById(int taskId)
        {
            using var context = new TaskManagerDbContext();
            return context.Tasks
                .Include(t => t.Type)
                .FirstOrDefault(t => t.Id == taskId);
        }
    }
}
