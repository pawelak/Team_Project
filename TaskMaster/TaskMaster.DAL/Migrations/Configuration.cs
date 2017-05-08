using System.Collections.Generic;
using TaskMaster.DAL.Models;
using System.Data.Entity.Migrations;

namespace TaskMaster.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TaskMaster.DAL.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskMaster.DAL.Context.DatabaseContext context)
        {
            IList<User> deflautUsers = new List<User>();

            deflautUsers.Add(new User() { Email = "dlanorberta@gmail.com", Name = "Norbercik" });
            deflautUsers.Add(new User() { Email = "dlapawela@gmail.com", Name = "Pawe³ek" });
            deflautUsers.Add(new User() { Email = "dlabartosza@gmail.com", Name = "Bartoszek" });

            foreach (User ELEM in deflautUsers) context.User.Add(ELEM);

            base.Seed(context);
        }
    }
}
