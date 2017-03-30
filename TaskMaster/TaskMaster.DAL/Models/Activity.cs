using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Activity
    {
        public Activity() { }
        [Key]
        public int Activityid { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
        public Task Task { get; set; }

        public ICollection<Parts_of_activity> Parts_of_activity { get; set; }
        
    }
}