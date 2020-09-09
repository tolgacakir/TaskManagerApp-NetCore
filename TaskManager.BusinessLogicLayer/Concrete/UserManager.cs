using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.Aspects.Autofac.Validation;
using TaskManagerApp.Core.CrossCuttingConcerns.Validation;
using TaskManagerApp.Core.Utilities.Security;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.Concrete
{
    public class UserManager : IUserService
    {
        //private readonly IValidator _validator;
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
            //_validator = new UserValidator();
        }

        [ValidationAspect(typeof(UserValidator))]
        public User Create(User user)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            //var user = new User
            //{
            //    Username = username,
            //    Password = password,
            //    PasswordHash = passwordHash,
            //    PasswordSalt = passwordSalt,
            //};
            //ValidationTool.Validate(_validator, user);

            return _userDal.Add(user);
        }

        [ValidationAspect(typeof(UserValidator))]
        public User Login(User tempUser)
        {
            //ValidationTool.Validate(_validator, user);
            User user = null;
            user = _userDal.Get(u => u.Username == tempUser.Username);
            bool isVerified = HashingHelper.VerifyPasswordHash(tempUser.Password, user.PasswordHash, user.PasswordSalt);

            return isVerified 
                ? user ?? null
                : null;
        }
    }
}
