using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class User
    {
        public User() {}
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
		
		public ICollection<Event> Event { get; set; }
        public ICollection<Action> Action { get; set; }
        public ICollection<Token> Token { get; set; }
    }
}
