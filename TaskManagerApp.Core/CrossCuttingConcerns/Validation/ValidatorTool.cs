using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApp.Core.CrossCuttingConcerns.Validation
{
    public class ValidatorTool
    {
        public static void Validate<T>(IValidator validator, T entity)
        {
            var context = new ValidationContext<T>(entity);
            var results = validator.Validate(context);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors[0].ErrorMessage.ToString());
            }
        }
    }

}
