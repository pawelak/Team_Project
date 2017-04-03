using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class UserGroup
    {
        public UserGroup() { }
        [Key]
        public int userGroupId { get; set; }

        public User user { get; set; }
        public Group group { get; set; }

    }
}