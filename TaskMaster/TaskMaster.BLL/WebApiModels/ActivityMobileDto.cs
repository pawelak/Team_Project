using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebApiModels
{
    public class ActivityMobileDto
    {
        public string Comment { get; set; }
        public string UserEmail { get; set; }
        public string GroupName { get; set; }
        public string TaskName { get; set; }

    }
}