using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.DataAccess;
using TaskManagerApp.Core.Entities;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
