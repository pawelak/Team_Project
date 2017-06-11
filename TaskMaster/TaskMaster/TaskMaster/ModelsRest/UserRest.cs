using TaskMaster.Enums;

namespace TaskMaster.ModelsRest
{
    public class UserRest
    {
        public string Email { get; set; }
        public string Description { get; set; }        
        public string Token { get; set; }
        public PlatformType PlatformType { get; set; }
    }
}
