
namespace TaskMaster.DAL.DTOModels
{
    public class FavoritesDto
    {
        public int FavoritesId { get; set; }

        public int UserId { get; set; }
        public virtual UserDto User { get; set; }
        public int TaskId { get; set; }
        public virtual TaskDto Task { get; set; }
    }
}