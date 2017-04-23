using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class GroupDto
    {
        public string NameGroup { get; set; }
        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<UserGroupDto> UserGroup { get; set; }
    }
}