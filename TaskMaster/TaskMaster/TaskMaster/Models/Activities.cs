using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskMaster.Models
{
    public class Activities
    {
        [PrimaryKey,AutoIncrement]
        public int activityId { get; set; }
        public string comment { get; set; }
        public int userId { get; set; }
        public int taskId { get; set; }
        public int groupId { get; set; }
        public int status { get; set; }
    }
}
