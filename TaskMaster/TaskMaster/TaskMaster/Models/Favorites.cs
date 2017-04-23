using SQLite;

namespace TaskMaster.Models
{
    public class Favorites
    {
        [PrimaryKey, AutoIncrement]
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
