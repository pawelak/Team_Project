using System.Collections.Generic;

namespace TaskMaster.DAL.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string NameGroup { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<UserGroup> UserGroup { get; set; }
    }
}