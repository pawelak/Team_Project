using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class ActivityDto
    {
        public string Comment { get; set; }
        public UserDto User { get; set; }
        public GroupDto Group { get; set; }
        public TaskDto Task { get; set; }
        public State State { get; set; }
        public ICollection<PartsOfActivityDto> PartsOfActivity { get; set; }  
    }

    public enum State
    {
        Planed,
        Started,
        Paused,
        Ended
    }
}