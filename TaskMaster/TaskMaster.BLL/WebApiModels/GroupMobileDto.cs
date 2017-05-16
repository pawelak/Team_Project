namespace TaskMaster.BLL.WebApiModels
{
    public class GroupMobileDto
    {
        public string NameGroup { get; set; }

        public string ActivityName { get; set; }
        public IList<UserGroupDto> UserGroup { get; set; }
    }
}