using System.Collections.Generic;
using TaskMaster.DAL.Enum;

namespace TaskMaster.DAL.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public State State { get; set; }
        public string Comment { get; set; }
        //public string GUID { get; set; }
         
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }

         
        public virtual ICollection<PartsOfActivity> PartsOfActivity { get; set; }
    }
}