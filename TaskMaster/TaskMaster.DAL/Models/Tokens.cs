namespace TaskMaster.DAL.Models
{
    public class Tokens
    {
        public Tokens() { }
        public int TokensId { get; set; }
        public string Token { get; set; }
        public BrowserType BrowserType { get; set; }
        public PlatformType PlatformType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }   
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
