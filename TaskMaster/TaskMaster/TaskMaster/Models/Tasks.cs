using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TaskMaster.Models
{
    public class Tasks
    {
        [PrimaryKey,AutoIncrement]
        public int taskId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Activities> activities { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Favorites> favorites { get; set; }
    }
}
