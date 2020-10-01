using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.Aspects.Castle.Validation;
using TaskManagerApp.Core.CrossCuttingConcerns.Validation;
using TaskManagerApp.Core.Utilities.Security;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public User Create(User user)
        {
            HashingHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return _userDal.Add(user);
        }

        [ValidationAspect(typeof(UserValidator))]
        public User Login(User user)
        {
            string password = user.Password;
            user = _userDal.Get(u => u.Username == user.Username);
            bool isVerified = HashingHelper.VerifyPasswordHash(password, user?.PasswordHash, user?.PasswordSalt);
            return isVerified
                ? user
                : null;
        }
    }
}
