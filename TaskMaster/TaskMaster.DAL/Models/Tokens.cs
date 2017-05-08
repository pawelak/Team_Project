using TaskMaster.DAL.Enum;

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
}
