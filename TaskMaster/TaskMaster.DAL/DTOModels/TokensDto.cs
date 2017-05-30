using TaskMaster.DAL.Enum;

namespace TaskMaster.DAL.DTOModels
{
    public class TokensDto
    {
        public int TokensId { get; set; }
        public string Token { get; set; }

        public BrowserType BrowserType { get; set; }
        public PlatformType PlatformType { get; set; }

        public UserDto User { get; set; }
    }
}