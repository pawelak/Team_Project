using System.Collections.Generic;

namespace TaskMaster.DAL.Models
{
    public class Group
    {
        public Group() { }
        public int GroupId { get; set; }
        public string NameGroup { get; set; }
        public ICollection<Activity> Activity { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }
    }
}