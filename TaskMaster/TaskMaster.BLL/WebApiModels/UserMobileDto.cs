using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebApiModels
{
    public class UserMobileDto
    {
        public string Email { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public PlatformType PlatformType { get; set; }

    }
}