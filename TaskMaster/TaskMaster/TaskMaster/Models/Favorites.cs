using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
using TaskMaster.Models;

namespace TaskMaster.Models
{
    public class Favorites
    {
        [PrimaryKey, AutoIncrement]
        public int favoriteId { get; set; }
        [ForeignKey(typeof(User))]
        public int userId { get; set; }
        [ForeignKey(typeof(Tasks))]
        public int taskId { get; set; }
        [ManyToOne]
        public User user { get; set; }
        [ManyToOne]
        public Tasks task { get; set;}
    }
}
