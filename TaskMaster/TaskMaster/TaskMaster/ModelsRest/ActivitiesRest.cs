using System.Collections.Generic;
using TaskMaster.Enums;

namespace TaskMaster.ModelsRest
{
    public class ActivitiesRest
    {
        public string Guid { get; set; }
        public string Comment { get; set; }
        public string UserEmail { get; set; }
        public string GroupName { get; set; }
        public string TaskName { get; set; }
        public string Token { get; set; }
        public EditState EditState { get; set; }
        public StatusType State { get; set; }
        public List<PartsOfActivityRest> TaskPartsList { get; set; }
    }
}
