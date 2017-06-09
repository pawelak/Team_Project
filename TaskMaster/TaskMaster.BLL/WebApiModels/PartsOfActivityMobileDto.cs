using System;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.BLL.WebApiModels
{
    public class PartsOfActivityMobileDto
    {
        public string Start { get; set; }
        public string Stop { get; set; }
        public string Duration { get; set; }

    }
}