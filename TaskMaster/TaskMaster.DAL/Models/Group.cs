using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Group
    {
        public Group() { }
        [Key]
        public int Groupid { get; set; }
        public string Name_group { get; set; }
 
        public ICollection<Activity> Activity { get; set; }
        public ICollection<User_Group> User_Group { get; set; }

    }
}