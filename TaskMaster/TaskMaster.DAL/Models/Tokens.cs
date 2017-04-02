using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Tokens
    {
        public Tokens() { }
        [Key]
        public int tokensId { get; set; }
        public string token { get; set; }
        public int kind { get; set; }
        public int platform { get; set; }

        public User user { get; set; }   
    }
}