using System.Collections.Generic;
using TaskMaster.DAL.Enum;

namespace TaskMaster.DAL.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public State State { get; set; }
        public EditState EditState { get; set; }
        public string Comment { get; set; }
        public string Guid { get; set; }
         
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        public virtual ICollection<PartsOfActivity> PartsOfActivity { get; set; }
    }
}