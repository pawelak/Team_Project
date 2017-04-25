using System.Collections.Generic;

namespace TaskMaster.DAL.Models
{
    public class User
    {
        public User() { }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }      
        public ICollection<Activity> Activity { get; set; }
        public ICollection<Tokens> Tokens { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
    }
}