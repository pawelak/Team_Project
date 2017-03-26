using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class EVENT
    {
        public EVENT() { }
        [Key]
        public int ID { get; set; }
        public ACTION ID_ACTION { get; set; }
        public USER ID_USER { get; set; }
        public ICollection<ACTIVITY> MY_ACTIVITY { get; set; }
    }
}
