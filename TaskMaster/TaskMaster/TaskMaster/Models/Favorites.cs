using SQLite;
using TaskMaster.Enums;

namespace TaskMaster.Models
{
    public class Favorites
    {
        [PrimaryKey, AutoIncrement]
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public EditState SyncStatus { get; set; }
    }
}
