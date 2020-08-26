using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.Entities;

namespace TaskManagerApp.Entities.Concrete
{
    public class TaskType : IEntity
    {
        public byte Id { get; set; }
        public string Description { get; set; }
    }
}
