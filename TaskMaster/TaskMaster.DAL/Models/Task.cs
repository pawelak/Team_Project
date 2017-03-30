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
        public int Taskid { get; set; }
        public string Name { get; set; }
        public string Descryption { get; set; }

        public ICollection<Activity> Activity { get; set; }
        public ICollection<Favorities> Favorites { get; set; }

    }
}