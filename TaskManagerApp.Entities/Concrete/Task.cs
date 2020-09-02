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

        public DateTime EndingDate => StartingDate.AddDays(Type.DaysCount -1);

        public int DifferenceFromEnd
        {
            get
            {
                return Convert.ToInt32((EndingDate.Date - DateTime.Now.Date).TotalDays);
            }
        }

        public int DifferenceFromStart
        {
            get
            {
                return Convert.ToInt32((StartingDate.Date - DateTime.Now.Date).TotalDays);
            }
        }

        public TaskStatuses Status
        {
            get
            {
                return GetTaskStatus();
            }
        }

        private TaskStatuses GetTaskStatus()
        {
            if (DateTime.Now.Date < StartingDate.Date)
            {
                return TaskStatuses.NotStarted;
            }
            else if (StartingDate.Date <= DateTime.Now.Date && DateTime.Now.Date <= EndingDate.Date)
            {
                return TaskStatuses.Active;
            }
            else if (EndingDate.Date < DateTime.Now.Date)
            {
                return TaskStatuses.OutOfDate;
            }
            else
            {
                throw new Exception("Invalid task status!");
            }
        }
    }


    public enum TaskStatuses
    {
        None=0,
        NotStarted=1,
        Active=2,
        OutOfDate=3,
    }
}
