using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation;
using TaskManagerApp.Core.CrossCuttingConcerns.Validation;
using TaskManagerApp.Core.Utilities.Security;
using TaskManagerApp.Entities.Concrete;
using Xunit;

namespace TaskManagerApp.BusinessLogicLayer.Tests
{
    public class TaskValidatorTests
    {
        IValidator _taskValidator;
        public TaskValidatorTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<IValidator, TaskValidator>();
            var serviceProvider = services.BuildServiceProvider();
            _taskValidator = serviceProvider.GetService<IValidator>();
        }

        [Fact]
        public void Should_Throw_Validation_Exception()
        {
            var task = new Task
            {
                Title = null,
            };

            Assert.Throws<ValidationException>(()=>ValidationTool.Validate(_taskValidator, task));
        }

    }
}
