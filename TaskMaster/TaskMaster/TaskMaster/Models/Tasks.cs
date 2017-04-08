using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskMaster.Models
{
    public class Tasks
    {
        [PrimaryKey,AutoIncrement]
        public int taskId { get; set; }
        [Unique]
        public string name { get; set; }
        public string description { get; set; }
    }
}
