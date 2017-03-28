using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Event
    {
        public Event() { }
        [Key]
        public int ID { get; set; }
		
        public Action Action { get; set; }
        public User User { get; set; }
		
        public ICollection<Activity> Activity { get; set; }
		public ICollection<Comment> Comment { get; set; }
    }
}
