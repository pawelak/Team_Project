using TaskMaster.Enums;

namespace TaskMaster.ModelsDto
{
    public class FavoritesDto
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public EditState SyncStatus { get; set; }
    }
}
