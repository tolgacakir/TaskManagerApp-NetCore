using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.WebUi.Models
{
    public class CreateEditTaskViewModel
    {
        public int UserId { get; set; }
        public Task Task { get; set; }
        public List<TaskType> TaskTypes { get; set; }

    }
}
