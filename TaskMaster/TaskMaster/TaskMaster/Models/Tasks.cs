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
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Events> Events { get; set; }

    }
}
