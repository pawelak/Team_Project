using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TaskMaster.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Events> Events { get; set; }
    }
}
