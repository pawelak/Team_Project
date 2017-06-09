using System.Collections.Generic;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebApiModels
{
    public class ActivityPlannedMobileDto
    {
        public string Comment { get; set; }
        public string UserEmail { get; set; }
        public string TaskName { get; set; }
        public string Token { get; set; }
        public string Guid { get; set; }

        public EditState EditState { get; set; }
        public State State { get; set; }

        // TODO inicjalizować w konstruktorze, jest to potrzebne ?
        public PartsOfActivityMobileDto TaskPart { get; set; }

    }
}