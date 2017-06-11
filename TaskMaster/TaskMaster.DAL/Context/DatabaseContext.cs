using System.Data.Entity;
using TaskMaster.DAL.Migrations;
using TaskMaster.DAL.Models;


namespace TaskMaster.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(): base("name=TaskMasterBase")
        {
            Database.SetInitializer(new DatabaseInitialize()); 
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = true;
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<PartsOfActivity> PartsOfActivity { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}