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
                Title = "unit_test_task_title",
                Description = "test test test",
                IsCompleted = false,
                StartingDate = DateTime.Now,
                TypeId = 1,
                UserId = 1
            };
            _taskManager.Add(task);

            bool result = _taskManager.GetAll(task.UserId)
                .Any(t => t.Title == task.Title && t.TypeId == task.TypeId && t.Description == task.Description);
            Assert.True(result);
        }

        [Fact]
        public void Should_Get_All_Task()
        {
            var count = _taskManager.GetAll(1)
                .Count();
            Assert.True(count>0);
        }

        [Fact]
        public void Should_Update_Task()
        {
            var task = _taskManager.GetAll(1)
                .Where(t => t.Id == 1)
                .FirstOrDefault();
            
            task.Description = "description after update test";
            _taskManager.Update(task);
            
            var updatedTask = _taskManager.GetAll(1)
                .Where(t => t.Id == 1)
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

    }
}
