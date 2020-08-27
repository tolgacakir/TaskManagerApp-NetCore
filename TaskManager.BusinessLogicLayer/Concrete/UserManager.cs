using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.BusinessLogicLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManager.BusinessLogicLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User Create(string username, string password)
        {
            return _userDal.Add(new User
            {
                Username = username,
                Password = password,
            });
        }

        public User Login(string username, string password)
        {
            var user = _userDal.Get(s => s.Username == username && s.Password == password);
            if (user==null)
            {
                throw new Exception("Invalid username or password.");
            }
            else
            {
                return user;
            }
        }

    }
}
