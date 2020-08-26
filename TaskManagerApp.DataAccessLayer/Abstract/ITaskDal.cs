using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.DataAccess;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Abstract
{
    public interface ITaskDal : IEntityRepository<Task>
    {
    }
}
