using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TaskMaster.Web.Models
{
    public class UsersDataBase : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}