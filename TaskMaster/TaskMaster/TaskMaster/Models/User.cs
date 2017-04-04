using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskMaster.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string TypeOfRegistration { get; set; }
    }
}
