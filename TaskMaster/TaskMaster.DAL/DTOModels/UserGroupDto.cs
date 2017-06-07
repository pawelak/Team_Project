
namespace TaskMaster.DAL.DTOModels
{
    public class UserGroupDto
    {
        public int UserGroupId { get; set; }

        public virtual UserDto User { get; set; }
        public virtual GroupDto Group { get; set; }
    }
}