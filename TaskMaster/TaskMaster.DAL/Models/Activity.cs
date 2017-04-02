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
        public int activityId { get; set; }
        public int state { get; set; }
        public string comment { get; set; }

        public User user { get; set; }
        public Group group { get; set; }
        public Task task { get; set; }

        public ICollection<PartsOfActivity> partsOfActivity { get; set; }
        
    }
}