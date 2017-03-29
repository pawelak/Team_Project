using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
using TaskMaster.Models;

namespace TaskMaster.Models
{
    public class Events
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [ForeignKey(typeof(User))]
        public int userId { get; set; }
        [ForeignKey(typeof(Tasks))]
        public int taskId { get; set; }
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<PartsOfTasks> parts { get; set; }
    }
}
