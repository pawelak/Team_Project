using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TaskMaster.Models
{
    public class PartsOfActivity
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        [ForeignKey(typeof(Activities))]
        public int activityID { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public DateTime duration { get; set; }
        [ManyToOne]
        public Activities activity { get; set; }
    }
}
