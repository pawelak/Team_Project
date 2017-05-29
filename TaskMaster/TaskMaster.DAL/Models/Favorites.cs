
namespace TaskMaster.DAL.Models
{
    public class Favorites
    {
        public int FavoritesId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}