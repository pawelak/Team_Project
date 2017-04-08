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
        public int favoriteId { get; set; }
        public int userId { get; set; }
        public int taskId { get; set; }
    }
}
