using System.Data.Entity;
using TaskMaster.DAL.Migrations;
using TaskMaster.DAL.Models;


namespace TaskMaster.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(): base("name=TaskMasterBase")
        {
            
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<PartsOfActivity> PartsOfActivity { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

    }
}