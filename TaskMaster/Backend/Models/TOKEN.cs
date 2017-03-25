using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TOKEN
    {
        public TOKEN() { }
        [Key]
        public int ID { get; set; }
        public USER ID_USER { get; set; }
        public string PASSWORD { get; set; }
    }
}
