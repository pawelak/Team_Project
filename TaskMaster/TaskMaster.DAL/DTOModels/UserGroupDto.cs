
namespace TaskMaster.DAL.DTOModels
{
    public class UserGroupDto
    {
        public int UserGroupId { get; set; }
        public UserDto User { get; set; }
        public GroupDto Group { get; set; }

    }
}