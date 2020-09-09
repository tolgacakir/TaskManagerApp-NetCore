using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApp.Core.Exceptions
{
    public class WrongValidationTypeException : Exception
    {

        public WrongValidationTypeException(string Message = "Wrong validator type.") : base(Message)
        {
        }
    }
}
