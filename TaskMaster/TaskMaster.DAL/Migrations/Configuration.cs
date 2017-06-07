using System.Data.Entity.Migrations;

namespace TaskMaster.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    
    }
}
