using TaskMaster.Enums;

namespace TaskMaster.ModelsDto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string TypeOfRegistration { get; set; }
        public bool IsLoggedIn { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public string ApiToken { get; set; }
    }
}
