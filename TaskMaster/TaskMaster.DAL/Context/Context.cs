using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TaskMaster.DAL
{
    public class Context : DbContext
    {
        public Context(): base("name=TaskMasterBase")
        {
            //Database.Connection.ConnectionString = "Data Source=DESKTOP-I62ETTQ;Initial Catalog=NOWA;User ID=Konserwator; Password=poziomkowa13";
            // Database.SetInitializer<CONNECTION>(new CreateDatabaseIfNotExists<CONNECTION>());
        }

        public DbSet<Activity> activity { get; set; }
        public DbSet<Favorities> favorities { get; set; }
        public DbSet<Group> group { get; set; }
        public DbSet<PartsOfActivity> partsOfActivity { get; set; }
        public DbSet<Task> task { get; set; }
        public DbSet<Tokens> tokens { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<UserGroup> userGroup { get; set; }

    }
}