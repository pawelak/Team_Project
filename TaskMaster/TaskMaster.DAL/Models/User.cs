using System.Collections.Generic;

namespace TaskMaster.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }      

        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Tokens> Tokens { get; set; }
        public virtual ICollection<UserGroup> UserGroup { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}