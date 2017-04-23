using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskMaster.DAL.Context;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            IList<User> deflautUsers = new List<User>();

            deflautUsers.Add(new User() {Email = "dlanorberta@gmail.com",Name = "Norbercik" });
            deflautUsers.Add(new User() {Email = "dlapawela@gmail.com" , Name = "Pawełek" });
            deflautUsers.Add(new User() {Email = "dlabartosza@gmail.com",Name = "Bartoszek" });

            foreach (User elem in deflautUsers) context.User.Add(elem);

            base.Seed(context);

        }

    }
}