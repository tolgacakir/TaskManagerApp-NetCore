using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.CrossCuttingConcerns.Validation;
using TaskManagerApp.Core.Utilities.Security;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Concrete
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
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Username = username,
                Password = password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            ValidatorTool.Validate(_validator, user);

            return _userDal.Add(user);
        }

        public User Login(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password,
            };
            ValidatorTool.Validate(_validator, user);
            user = null;
            user = _userDal.Get(u => u.Username == username);
            bool isVerified = HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

            return isVerified 
                ? user ?? null
                : null;
        }
    }
}
