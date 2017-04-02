using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DAL
{
    public class Action
    {
        public Action() { }
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
		public string Category { get; set; }
        public string Desc { get; set; }
		public bool Group { get; set; }
		
		public User User { get; set; }
		
        public ICollection<Event> Event { get; set; }
		public ICollection<Comment> Comment { get; set; }
    }
}
