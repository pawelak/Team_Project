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
        public int groupId { get; set; }
        public string nameGroup { get; set; }
 
        public ICollection<Activity> activity { get; set; }
        public ICollection<UserGroup> userGroup { get; set; }

    }
}