using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Task
    {
        public Task() { }
        [Key]
        public int taskId { get; set; }
        public string name { get; set; }
        public string descryption { get; set; }

        public ICollection<Activity> activity { get; set; }
        public ICollection<Favorities> favorites { get; set; }

    }
}