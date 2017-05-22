using SQLite;

namespace TaskMaster.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string TypeOfRegistration { get; set; }
        public bool IsLoggedIn { get; set; }
        public SyncStatusType SyncStatus { get; set; }
    }
}
