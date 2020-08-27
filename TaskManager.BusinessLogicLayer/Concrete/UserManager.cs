using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.BusinessLogicLayer.Abstract;
using TaskManager.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.CrossCuttingConcerns;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManager.BusinessLogicLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IValidator _validator;
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
            _validator = new UserValidator();
        }

        public User Create(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password,
            };
            ValidatorTool.Validate(_validator, user);

            return _userDal.Add(user);
        }

        public User Login(string username, string password)
        {
            var tempUser = new User
            {
                Username = username,
                Password = password,
            };
            ValidatorTool.Validate(_validator, tempUser);

            var user = _userDal.Get(s => s.Username == tempUser.Username && s.Password == tempUser.Password);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Invalid username or password.");
            }
        }

        

    }
}
