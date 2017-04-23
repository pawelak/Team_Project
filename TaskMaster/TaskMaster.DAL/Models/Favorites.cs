namespace TaskMaster.DAL.Models
{
    public class Favorites
    {
        public Favorites() { }
        public int FavoritesId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }

    }
}