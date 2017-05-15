using System.Collections.Generic;
using TaskMaster.DAL.Models;
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

        protected override void Seed(TaskMaster.DAL.Context.DatabaseContext context)
        {
            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Email = "dlanorberta@gmail.com", Name = "Norbercik" });
            defaultUsers.Add(new User() { Email = "dlapawela@gmail.com", Name = "Pawe³ek" });
            defaultUsers.Add(new User() { Email = "dlabartosza@gmail.com", Name = "Bartoszek" });

            foreach (var elem in defaultUsers) context.User.Add(elem);

            IList<Tokens> defaultTokens = new List<Tokens>();

            defaultTokens.Add(new Tokens() { Token = "123", User = defaultUsers[1] });
            defaultTokens.Add(new Tokens() { Token = "abc", User = defaultUsers[2] });
            defaultTokens.Add(new Tokens() { Token = "+*+", User = defaultUsers[0] });

            foreach (var elem in defaultUsers) context.User.Add(elem);

            base.Seed(context);
        }
    }
}
