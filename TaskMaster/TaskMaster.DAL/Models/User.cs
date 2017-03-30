using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class User
    {
        public User() { }
        [Key]
        public int Userid { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public ICollection<Activity> Activity { get; set; }
        public ICollection<Tokens> Tokens { get; set; }
        public ICollection<User_Group> User_Group { get; set; }
        public ICollection<Favorities> Favorities { get; set; }

    }
}