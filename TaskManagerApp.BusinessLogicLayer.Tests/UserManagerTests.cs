using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.Concrete;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Concrete.EntityFramework;
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
        public void Should_Be_Successfull_Login()
        {
            string username = "test_user";
            string password = "22222222";
            var user = _userManager.Login(username, password);

            Assert.NotNull(user);
        }

        [Fact]
        public void Should_Be_Unsuccessfull_Login()
        {
            string username = "test_user";
            string password = "22222223";
            var user = _userManager.Login(username, password);

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
