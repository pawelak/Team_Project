using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
	public class Activity
    {
        Activity() { }
        [Key]
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Time { get; set; }
		
		public Event Event { get; set; }
    }
}
