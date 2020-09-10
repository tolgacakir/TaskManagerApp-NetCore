using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.Concrete;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Concrete.EntityFramework;
using TaskManagerApp.Entities.Concrete;
using Xunit;

namespace TaskManagerApp.BusinessLogicLayer.Tests
{
    public class UserManagerTests
    {
        private readonly IUserService _userManager;
        public UserManagerTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IUserDal, EfUserDal>();
            var serviceProvider = services.BuildServiceProvider();
            _userManager = serviceProvider.GetService<IUserService>();
        }
        [Fact]
        public void Should_Login()
        {
            var tempUser = new User()
            {
                Username = "test_user",
                Password = "22222222",
            };
            var user = _userManager.Login(tempUser);
            Assert.NotNull(user);
        }

        [Fact]
        public void Should_Not_Login_For_Wrong_Username()
        {
            var user = new User()
            {
                Username = "1",
                Password = "--------",
            };

            user = _userManager.Login(user);
            Assert.Null(user);
        }

        [Fact]
        public void Should_Not_Login_For_Wrong_Password()
        {
            var user = new User()
            {
                Username = "test_user",
                Password = "--------",
            };

            user = _userManager.Login(user);
            Assert.Null(user);
        }

        [Fact]
        public void Should_Not_Login_For_Null_User()
        {
            User user = null;
            Assert.Throws<NullReferenceException>(() =>
            {
                user = _userManager.Login(user);
            });
        }

        [Fact]
        public void Should_Not_Login_For_Null_Password()
        {
            var user = new User()
            {
                Username = "test_user",
                Password = null,
            };

            user = _userManager.Login(user);
            Assert.Null(user);
        }

        [Fact]
        public void Should_Not_Login_For_Empty_Password()
        {
            var user = new User()
            {
                Username = "test_user",
                Password = "",
            };

            user = _userManager.Login(user);
            Assert.Null(user);
        }

        //[Fact]
        //public void Should_Create_User()
        //{
        //    string username = "test_user";
        //    string password = "22222222";
        //    var user = _userManager.Create(username, password);

        //    Assert.NotNull(user);
        //}
    }
}
