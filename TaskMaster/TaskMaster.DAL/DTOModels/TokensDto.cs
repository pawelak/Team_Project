namespace TaskMaster.DAL.DTOModels
{
    public class TokensDto
    {
        public string Token { get; set; }
        public BrowserType BrowserType { get; set; }
        public PlatformType PlatformType { get; set; }
        public UserDto User { get; set; }
    }

        public enum BrowserType
        {
            Firefox,
            Chrome,
            Opera,
            Safari
        }

        public enum PlatformType
        {
            Android,
            WindowsPhone,
            Ios
        }
   
}