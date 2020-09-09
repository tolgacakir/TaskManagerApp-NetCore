using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using TaskManagerApp.Core.DataAccess.EntityFramework;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework
{
    public class EfTaskDal : EfEntityRepositoryBase<Task,TaskManagerDbContext>, ITaskDal
    {
        
    }
}
