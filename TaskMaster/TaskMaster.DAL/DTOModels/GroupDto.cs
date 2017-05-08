using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class GroupDto
    {
        public string NameGroup { get; set; }
 
        public IList<ActivityDto> Activity { get; set; }
        public IList<UserGroupDto> UserGroup { get; set; }
    }
}