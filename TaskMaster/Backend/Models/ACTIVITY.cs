using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
	public class ACTIVITY
    {
        ACTIVITY() { }
        [Key]
        public int ID { get; set; }
        public EVENT ID_EVENT { get; set; }
        public DateTime START { get; set; }
        public DateTime END { get; set; }
    }
}
