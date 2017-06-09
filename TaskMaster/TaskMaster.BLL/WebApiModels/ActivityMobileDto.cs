using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebApiModels
{
    public class ActivityMobileDto
    {
        public string UserEmail { get; set; }
        public string Comment { get; set; }
        public string TaskName { get; set; }
        public string Token { get; set; }
        public string Guid { get; set; }
            
        public EditState EditState { get; set; }
        public State State { get; set; }

        // TODO lepiej w modelu mieć IEnumerable i inicjalizwoać w konstruktorze
        public List<PartsOfActivityMobileDto> TaskPartsList{ get; set; } 

    }
}