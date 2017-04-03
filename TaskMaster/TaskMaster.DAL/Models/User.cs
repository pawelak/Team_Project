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
        public int userId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public ICollection<Activity> activity { get; set; }
        public ICollection<Tokens> tokens { get; set; }
        public ICollection<UserGroup> userGroup { get; set; }
        public ICollection<Favorities> favorities { get; set; }

    }
}