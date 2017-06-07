
namespace TaskMaster.DAL.DTOModels
{
    public class FavoritesDto
    {
        public int FavoritesId { get; set; }

        public virtual UserDto User { get; set; }
        public virtual TaskDto Task { get; set; }
    }
}