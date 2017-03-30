using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskMaster.Models
{
    public class PartsOfActivity
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public int activityID { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public DateTime duration { get; set; }
    }
}
