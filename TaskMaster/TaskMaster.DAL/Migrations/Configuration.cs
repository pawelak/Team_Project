using System.Collections;
using System.Collections.Generic;
using TaskMaster.DAL.Models;
using System.Data.Entity.Migrations;
using TaskMaster.DAL.Enum;

namespace TaskMaster.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(TaskMaster.DAL.Context.DatabaseContext context)
        {
            //List<User> defaultUsers = new List<User>();


            //var tmpUser = new User() { Email = "a@gmail.com", Name = "a", Tokens = new List<Tokens>()};
            //var tmpToken = new Tokens() { Token = "aaa", BrowserType = BrowserType.none, PlatformType = PlatformType.Android };
            //tmpUser.Tokens.Add(tmpToken);

            //context.User.Add(tmpUser);
           // foreach (var elem in defaultUsers) context.User.Add(elem);

           // base.Seed(context);
        }
    }
}
