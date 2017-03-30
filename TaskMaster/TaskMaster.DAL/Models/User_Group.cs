using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class User_Group
    {
        public User_Group() { }
        [Key]
        public int User_Groupid { get; set; }
        
        public User User { get; set; }
        public Group Group { get; set; }
 
    }
}