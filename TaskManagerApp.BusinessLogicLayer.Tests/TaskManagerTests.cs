using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.Concrete;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Concrete.EntityFramework;
using TaskManagerApp.Entities.Concrete;
using Xunit;

namespace TaskManagerApp.BusinessLogicLayer.Tests
{
    public class TaskManagerTests
    {
        private readonly ITaskService _taskManager;

        public TaskManagerTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITaskService, TaskManager>();
            services.AddTransient<ITaskDal, EfTaskDal>();
            var serviceProvider = services.BuildServiceProvider();
            _taskManager = serviceProvider.GetService<ITaskService>();
        }


        [Fact]
        public void Should_Add_Task()
        {
            var task = new Task()
            {
                Title = "unit_test_task_titl5",
                Description = "test test test",
                IsCompleted = false,
                StartingDate = DateTime.Now,
                TypeId = 1,
                UserId = 8
            };
            _taskManager.Add(task);

            bool result = _taskManager.GetAll(task.UserId)
                .Any(t => t.Title == task.Title && t.TypeId == task.TypeId && t.Description == task.Description &&t.StartingDate.Date == task.StartingDate.Date);
            Assert.True(result);
        }

        [Fact]
        public void Should_Get_All_Task()
        {
            var count = _taskManager.GetAll(8)
                .Count();
            Assert.True(count>0);
        }

        [Fact]
        public void Should_Update_Task()
        {
            var task = _taskManager.GetAll(8)
                .FirstOrDefault();

            task.Description += "1";
            _taskManager.Update(task);

            var updatedTask = _taskManager.GetAll(8)
                .Where(t => t.Id == task.Id)
                .FirstOrDefault();

            Assert.Equal(task.Description, updatedTask.Description);
        }


        [Fact]
        public void Should_Delete_Task()
        {
            var task = new Task
            {
                Title = "Task for delete test",
                TypeId = 1,
                StartingDate = DateTime.Now,
                UserId = 1,
            };

            _taskManager.Add(task);
            var tasks = _taskManager.GetAll(1);
            int count = tasks.Count;

            _taskManager.Delete(
                tasks.Where(t => t.Title == task.Title).SingleOrDefault().Id);
            int newCount = _taskManager.GetAll(1).Count;

            Assert.Equal(count - 1, newCount);
        }

        [Fact]
        public void Should_Not_Success_Transactional_Operation_Example()
        {
            bool result = false;
            try
            {
                var task = _taskManager.GetListWithType(8).FirstOrDefault();
                task.Description +="1";
                _taskManager.TransactionalOperationExample(task);
                result = true;
            }
            catch(Exception e)
            {
            }
            Assert.False(result);
        }
    }
}
