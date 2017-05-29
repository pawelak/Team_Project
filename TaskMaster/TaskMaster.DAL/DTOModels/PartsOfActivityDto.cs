using System;

namespace TaskMaster.DAL.DTOModels
{
    public class PartsOfActivityDto
    {
        public int PartsOfActivityId { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public TimeSpan Duration { get; set; }

        public ActivityDto Activity { get; set; }
    }
}