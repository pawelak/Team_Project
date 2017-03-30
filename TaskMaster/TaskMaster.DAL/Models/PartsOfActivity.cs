using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class PartsOfActivity
    {
        public PartsOfActivity() { }
        [Key]
        public int PartsOfActivityId { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }

        public Activity Activity { get; set; }

    }
}