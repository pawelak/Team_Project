using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class Connection : DbContext
    {
        public Connection(): base("name=TaskMasterBase")
        {
           //Database.Connection.ConnectionString = "Data Source=DESKTOP-I62ETTQ;Initial Catalog=NOWA;User ID=Konserwator; Password=poziomkowa13";
           // Database.SetInitializer<CONNECTION>(new CreateDatabaseIfNotExists<CONNECTION>());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Action> Action { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Token> Token { get; set; }
		public DbSet<Comment> Comment { get; set; }


    }
}
