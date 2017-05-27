using System;
using System.Collections.Generic;
using System.Data.Entity;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Context
{
    public class DatabaseInitialize : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(Context.DatabaseContext context)
        {
            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() { Email = "dlanorberta@gmail.com", Name = "Norbercik" });
            defaultUsers.Add(new User() { Email = "dlapawela@gmail.com", Name = "Pawełek" });
            defaultUsers.Add(new User() { Email = "dlabartosza@gmail.com", Name = "Bartoszek" });

            foreach (var elem in defaultUsers) context.User.Add(elem);


            IList<Tokens> defaultTokens = new List<Tokens>();

            defaultTokens.Add(new Tokens() { Token = "123", User = defaultUsers[1] });
            defaultTokens.Add(new Tokens() { Token = "abc", User = defaultUsers[2] });
            defaultTokens.Add(new Tokens() { Token = "+*+", User = defaultUsers[0] });

            foreach (var elem in defaultTokens) context.Tokens.Add(elem);

            IList<Group> defaultGroup = new List<Group>();

            defaultGroup.Add(new Group() { NameGroup = "NiedzielniGracze" });

            foreach (var elem in defaultGroup) context.Group.Add(elem);

            IList<Task> defaultTasks = new List<Task>();

            defaultTasks.Add(new Task() { Name = "Granie" });

            foreach (var elem in defaultTasks) context.Task.Add(elem);

            IList<Activity> defaultActivity = new List<Activity>();

            defaultActivity.Add(new Activity() { Comment = "Co sie dzieje", User = defaultUsers[0], Task = defaultTasks[0] });
            defaultActivity.Add(new Activity() { Comment = "Co sie bedzie dziac", User = defaultUsers[0], Task = defaultTasks[0] });
            defaultActivity.Add(new Activity() { Comment = "Co sie stalo", User = defaultUsers[0], Task = defaultTasks[0] });

            foreach (var elem in defaultActivity) context.Activity.Add(elem);

            IList<PartsOfActivity> defaultPartsOfActivity = new List<PartsOfActivity>();

            defaultPartsOfActivity.Add(new PartsOfActivity() { Activity = defaultActivity[2], Duration = new TimeSpan(5, 0, 20) });
            defaultPartsOfActivity.Add(new PartsOfActivity() { Activity = defaultActivity[0], Duration = new TimeSpan(0, 2, 30) });
            defaultPartsOfActivity.Add(new PartsOfActivity() { Activity = defaultActivity[1], Duration = new TimeSpan(0, 5, 40) });

            foreach (var elem in defaultPartsOfActivity) context.PartsOfActivity.Add(elem);

            base.Seed(context);
        }


    }
}