using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Favorities
    {
        public Favorities() { }
        [Key]
        public int Favoritiesid { get; set; }

        public User User { get; set; }
        public Task Task { get; set; }

    }
}