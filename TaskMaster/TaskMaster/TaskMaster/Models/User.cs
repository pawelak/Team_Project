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
        public int userId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string token { get; set; }
        public string kind { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Activities> activities { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Favorites> favorites { get; set; }
    }
}
