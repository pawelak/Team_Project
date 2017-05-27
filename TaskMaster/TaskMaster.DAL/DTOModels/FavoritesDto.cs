
namespace TaskMaster.DAL.DTOModels
{
    public class FavoritesDto
    {
        public int FavoritesId { get; set; }
        public UserDto User { get; set; }
        public TaskDto Task { get; set; }
    }
}