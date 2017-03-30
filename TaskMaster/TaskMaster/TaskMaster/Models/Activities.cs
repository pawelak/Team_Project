using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TaskMaster.Models
{
    public class Activities
    {
        [PrimaryKey,AutoIncrement]
        public int activityId { get; set; }
        public string comment { get; set; }
        [ForeignKey(typeof(User))]
        public int userId { get; set; }
        [ForeignKey(typeof(Tasks))]
        public int taskId { get; set; }
        public int groupId { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PartsOfActivity> partsOfActivity { get; set; }
        [ManyToOne]
        public User user { get; set; }
        [ManyToOne]
        public Tasks task { get; set; }
    }
}
