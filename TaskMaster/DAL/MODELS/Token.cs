using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Token
    {
        public Token() { }
        [Key]
        public int ID { get; set; }
		public string Login { get; set; }
        public string Pass { get; set; }
		
		public User User { get; set; }
    }
}
