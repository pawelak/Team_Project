using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<ActivityDto> Activities { get; set; }
        public virtual ICollection<FavoritesDto> Favorites { get; set; }
    }
}