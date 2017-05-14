using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class TaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<ActivityDto> Activity { get; set; }
        public IList<FavoritesDto> Favorites { get; set; }
    }
}