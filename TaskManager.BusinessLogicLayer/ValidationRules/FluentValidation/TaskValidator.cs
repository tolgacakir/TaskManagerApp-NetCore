using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation
{
    public class TaskValidator : AbstractValidator<Task>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Title).NotEmpty().WithMessage("Title can not be empty.");
            RuleFor(t => t.Title).NotNull().WithMessage("Title can not be null.");
            RuleFor(t => t.Title).Length(1, 20).WithMessage("Title must be between 1-20 characters.");
        }
    }
}
