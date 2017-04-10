using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMaster.ModelsDto
{
    public class FavoritesDto
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
