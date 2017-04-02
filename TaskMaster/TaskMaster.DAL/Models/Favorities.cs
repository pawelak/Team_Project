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
        public int favoritiesId { get; set; }

        public User user { get; set; }
        public Task task { get; set; }

    }
}