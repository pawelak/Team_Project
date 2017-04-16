using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class TaskDto
    {       
        public string Name { get; set; }
        public string Descryption { get; set; }
        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<FavoritesDto> Favorites { get; set; }

    }
}