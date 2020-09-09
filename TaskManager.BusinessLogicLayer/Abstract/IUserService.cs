using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Abstract
{
    public interface IUserService
    {
        User Login(User tempUser);
        User Create(User user);
    }
}
