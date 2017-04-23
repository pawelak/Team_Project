using System;

namespace TaskMaster.DAL.Models
{
    public class PartsOfActivity
    {
        public PartsOfActivity() { }
        public int PartsOfActivityId { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}