using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.DataAccess.EntityFramework;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework
{
    public class EfTaskDal : EfEntityRepositoryBase<Task,TaskManagerDbContext>, ITaskDal
    {
    }
}
