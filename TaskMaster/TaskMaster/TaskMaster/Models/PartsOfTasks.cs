using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TaskMaster.Models
{
    public class PartsOfTasks
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        [ForeignKey(typeof(Events))]
        public int eventID { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public DateTime duration { get; set; }
    }
}
