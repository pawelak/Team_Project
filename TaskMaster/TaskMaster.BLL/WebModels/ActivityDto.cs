using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebModels
{
    public class ActivityDto
    {
        public string UserEmail { get; set; }
        public string Comment { get; set; }
        public string TaskName { get; set; }
        public string Token { get; set; }
        public string Guid { get; set; }

        public EditState EditState { get; set; }
        public State State { get; set; }

        public List<PartsOfActivityDto> TaskPartsList { get; set; }
    }
}
