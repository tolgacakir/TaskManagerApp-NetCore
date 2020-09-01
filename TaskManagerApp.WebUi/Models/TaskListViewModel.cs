using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.WebUi.Models
{
    public class TaskListViewModel
    {
        public User User { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
