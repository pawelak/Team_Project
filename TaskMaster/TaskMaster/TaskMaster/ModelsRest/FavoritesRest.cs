using System.Collections.Generic;
using TaskMaster.Enums;

namespace TaskMaster.ModelsRest
{
    public class FavoritesRest
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public EditState EditState { get; set; }
        public TasksRest Task { get; set; }
    }
}
