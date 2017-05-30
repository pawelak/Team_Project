using System.Collections.Generic;

namespace TaskMaster.DAL.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}