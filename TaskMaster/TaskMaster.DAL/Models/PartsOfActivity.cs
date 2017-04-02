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
        public int partsOfActivityId { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public DateTime duration { get; set; }

        public Activity activity { get; set; }

    }
}