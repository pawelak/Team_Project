using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TaskMaster.Models;

namespace TaskMaster.Models
{
    public class Favorites
    {
        [PrimaryKey, AutoIncrement]
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
