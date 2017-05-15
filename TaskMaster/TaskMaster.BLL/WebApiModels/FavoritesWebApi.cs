using System.Collections.Generic;

namespace TaskMaster.WebApi.Models
{
    public class FavoritesWebApi
    {
        public string UserEmail { get; set; }
        public ICollection<TaskWebApi> Tasks { get; set; }
    }
}