using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public string NameGroup { get; set; }

        public virtual ICollection<ActivityDto> Activities { get; set; }
        public virtual ICollection<UserGroupDto> UserGroup { get; set; }
    }
}