using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.Entities;

namespace TaskManagerApp.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
