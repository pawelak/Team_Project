using System;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.BLL.WebApiModels
{
    public class PartsOfActivityMobileDto
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }

        public ActivityDto Activity { get; set; }
    }
}