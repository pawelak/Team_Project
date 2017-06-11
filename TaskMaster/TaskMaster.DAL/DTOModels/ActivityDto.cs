using System.Collections.Generic;
using TaskMaster.DAL.Enum;

namespace TaskMaster.DAL.DTOModels
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public State State { get; set; }
        public EditState EditState { get; set; }
        public string Comment { get; set; }
        public string Guid { get; set; }
         
        public virtual UserDto User { get; set; }
        public virtual GroupDto Group { get; set; }
        public virtual TaskDto Task { get; set; }

        public virtual ICollection<PartsOfActivityDto> PartsOfActivity { get; set; }
    }
}