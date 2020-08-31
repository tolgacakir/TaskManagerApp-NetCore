using System;
using TaskManagerApp.Entities.Concrete;
using Xunit;

namespace TaskManagerApp.Entities.Tests
{
    public class TaskTests
    {
        [Fact]
        public void Should_Be_NotStarted()
        {
            var taskType = new TaskType
            {
                DaysCount = 1,
            };
            var task = new Task
            {
                Title = "test",
                StartingDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Type = taskType,
            };

            Assert.Equal(TaskStatuses.NotStarted, task.Status);
        }


        [Fact]
        public void Should_Be_Active()
        {
            var taskType = new TaskType
            {
                DaysCount = 7,
            };
            var task = new Task
            {
                Title = "test",
                StartingDate = DateTime.Now.AddDays(-1),
                IsCompleted = false,
                Type = taskType,
            };

            Assert.Equal(TaskStatuses.Active, task.Status);
        }


        [Fact]
        public void Should_Be_OutOfDate()
        {
            var taskType = new TaskType
            {
                DaysCount = 1,
            };
            var task = new Task
            {
                Title = "test",
                StartingDate = DateTime.Now.AddDays(-2),
                IsCompleted = false,
                Type = taskType,
            };

            Assert.Equal(TaskStatuses.OutOfDate, task.Status);
        }
    }
}
