using System.Collections.Generic;
using TaskMaster.DAL.Enum;

namespace TaskMaster.DAL.DTOModels
{
    public class ActivityDto
    {
        public State State { get; set; }
        public string Comment { get; set; }

        public UserDto User { get; set; }
        public GroupDto Group { get; set; }
        public TaskDto Task { get; set; }

        public IList<PartsOfActivityDto> PartsOfActivity { get; set; }
    }
}