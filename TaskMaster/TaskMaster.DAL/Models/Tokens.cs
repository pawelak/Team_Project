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
        public int Tokensid { get; set; }
        public string Token { get; set; }
        public int Kind { get; set; }
        public int Platform { get; set; }

        public User User { get; set; }   
    }
}