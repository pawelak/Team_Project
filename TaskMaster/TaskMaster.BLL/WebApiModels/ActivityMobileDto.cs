using System.Collections.Generic;
using Newtonsoft.Json;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebApiModels
{
    public class ActivityMobileDto
    {
        [JsonProperty("test")]
        public string UserEmail { get; set; }
        public string Comment { get; set; }
        public string TaskName { get; set; }
        public string Token { get; set; }
        public string Guid { get; set; }
            
        public EditState EditState { get; set; }
        public State State { get; set; }

        public List<PartsOfActivityMobileDto> TaskPartsList{ get; set; } 

    }
}