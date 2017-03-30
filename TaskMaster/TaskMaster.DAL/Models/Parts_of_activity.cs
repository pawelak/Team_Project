using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Parts_of_activity
    {
        public Parts_of_activity() { }
        [Key]
        public int Parts_of_activityid { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }

        public Activity Activity { get; set; }

    }
}