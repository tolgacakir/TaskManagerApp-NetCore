using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.BusinessLogicLayer.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username can not be empty.");
            RuleFor(u => u.Username).NotNull().WithMessage("Username can not be null.");
            RuleFor(u => u.Username).Length(3, 10).WithMessage("Username must be between 3-10 characters.");
            RuleFor(u => u.Username.Contains(" ")).Equal(false).WithMessage("Username can not contain space.");

            RuleFor(u => u.Username).NotEmpty().WithMessage("Password can not be empty.");
            RuleFor(u => u.Username).NotNull().WithMessage("Password can not be null.");
            RuleFor(u => u.Username).Length(8, 20).WithMessage("Password must be between 8-20 characters.");
            RuleFor(u => u.Username.Contains(" ")).Equal(false).WithMessage("Password can not contain space.");
        }
    }
}
