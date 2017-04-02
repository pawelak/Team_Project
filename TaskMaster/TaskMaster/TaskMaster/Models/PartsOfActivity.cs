using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskMaster.Models
{
    public class PartsOfActivity
    {
        [PrimaryKey,AutoIncrement]
        public int partId { get; set; }
        public int activityID { get; set; }
        public string start { get; set; }
        public string stop { get; set; }
        public string duration { get; set; }
    }
}
