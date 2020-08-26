using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.Entities;

namespace TaskManagerApp.Entities.Concrete
{
    public class Task : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte TypeId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime StartingDate { get; set; }
        public TaskType Type { get; set; }

    }
}
