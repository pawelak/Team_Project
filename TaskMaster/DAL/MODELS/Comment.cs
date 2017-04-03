using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Comment
    {
        public Comment() { }
        [Key]
        public int ID { get; set; }
        public bool Kind { get; set; }
        public string Desc { get; set; }
		
        public Event Event { get; set; }
		public Action Action { get; set; }
    }
}
