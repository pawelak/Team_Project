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

        public DbSet<Activity> Activity { get; set; }
        public DbSet<Favorities> Favorities { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<PartsOfActivity> PartsOfActivity { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<User_Group> User_group { get; set; }

    }
}