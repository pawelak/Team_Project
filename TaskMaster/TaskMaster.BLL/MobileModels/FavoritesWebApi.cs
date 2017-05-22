using System.Collections.Generic;

namespace TaskMaster.BLL.MobileModels
{
    public class FavoritesWebApi
    {
        public string UserEmail { get; set; }
        public ICollection<TaskWebApi> Tasks { get; set; }
    }
}