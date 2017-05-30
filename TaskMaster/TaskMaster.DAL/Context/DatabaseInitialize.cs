using System;
using System.Collections.Generic;
using System.Data.Entity;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Context
{
    public class DatabaseInitialize : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(Context.DatabaseContext context)
        {
            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User()
            {
                Email = "dlanorberta@gmail.com",
                Name = "Norbercik",
                Description = "testowy nr 1",
            });
            defaultUsers.Add(new User()
            {
                Email = "dlapawela@gmail.com",
                Name = "Pawełek",
                Description = "testowy nr 1",
            });
            defaultUsers.Add(new User()
            {
                Email = "dlabartosza@gmail.com",
                Name = "Bartoszek",
                Description = "testowy nr 1",
            });
            defaultUsers.Add(new User()
            {
                Email = "a@a.pl",
                Name = "alfa",
                Description = "testowy nr 1",
            });
            defaultUsers.Add(new User()
            {
                Email = "b@b.pl",
                Name = "Beta",
                Description = "testowy nr 2",
            });

            foreach (var elem in defaultUsers) context.User.Add(elem);



            IList<Tokens> defaultTokens = new List<Tokens>();

            defaultTokens.Add(new Tokens()
            {
                Token = "123",
                User = defaultUsers[1],
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "abc",
                User = defaultUsers[2],
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "+*+",
                User = defaultUsers[3],
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "qwe1",
                User = defaultUsers[4],
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "qwe2",
                User = defaultUsers[4],
                BrowserType = BrowserType.Firefox,
                PlatformType = PlatformType.Ios
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "qwe3",
                User = defaultUsers[0],
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "qwe4",
                User = defaultUsers[0],
                BrowserType = BrowserType.Firefox,
                PlatformType = PlatformType.Ios
            });
            defaultTokens.Add(new Tokens()
            {
                Token = "qwe5",
                User = defaultUsers[1],
                BrowserType = BrowserType.Firefox,
                PlatformType = PlatformType.Ios
            });

            foreach (var elem in defaultTokens) context.Tokens.Add(elem);



            IList<Group> defaultGroup = new List<Group>();

            defaultGroup.Add(new Group()
            {
                NameGroup = "NiedzielniGracze"
            });
            defaultGroup.Add(new Group()
            {
                NameGroup = "nazwa1",
            });
            defaultGroup.Add(new Group()
            {
                NameGroup = "nazwa2",
            });
            defaultGroup.Add(new Group()
            {
                NameGroup = "nazwa3",
            });

            foreach (var elem in defaultGroup) context.Group.Add(elem);



            IList<UserGroup> defaultUserGroup = new List<UserGroup>();

            defaultUserGroup.Add(new UserGroup()
            {
                User = defaultUsers[0],
                Group = defaultGroup[0]
            });
            defaultUserGroup.Add(new UserGroup()
            {
                User = defaultUsers[0],
                Group = defaultGroup[1]
            });
            defaultUserGroup.Add(new UserGroup()
            {
                User = defaultUsers[1],
                Group = defaultGroup[0]
            });
            defaultUserGroup.Add(new UserGroup()
            {
                User = defaultUsers[2],
                Group = defaultGroup[1]
            });

            foreach (var elem in defaultUserGroup) context.UserGroup.Add(elem);



            IList<Task> defaultTasks = new List<Task>();

            defaultTasks.Add(new Task()
            {
                Name = "Granie",
                Type = "typ1",
                Description = "Opis 1"
            });
            defaultTasks.Add(new Task()
            {
                Name = "biega1",
                Type = "typ1",
                Description = "Opis 2"
            });
            defaultTasks.Add(new Task()
            {
                Name = "biega2",
                Type = "typ2",
                Description = "Opis 3"
            });
            defaultTasks.Add(new Task()
            {
                Name = "biega3",
                Type = "typ3",
                Description = "Opis 4"
            });
            defaultTasks.Add(new Task()
            {
                Name = "biega4",
                Type = "typ4",
                Description = "Opis 5"
            });
            defaultTasks.Add(new Task()
            {
                Name = "biega5 do Fav",
                Type = "typ5",
                Description = "Opis 6"
            });
            defaultTasks.Add(new Task()
            {
                Name = "biega6 do Fav",
                Type = "typ6",
                Description = "Opis 7"
            });

            foreach (var elem in defaultTasks) context.Task.Add(elem);



            IList<Favorites> defaultFavorites = new List<Favorites>();

            defaultFavorites.Add(new Favorites()
            {
                User = defaultUsers[0],
                Task = defaultTasks[0]
            });
            defaultFavorites.Add(new Favorites()
            {
                User = defaultUsers[0],
                Task = defaultTasks[1]
            });
            defaultFavorites.Add(new Favorites()
            {
                User = defaultUsers[1],
                Task = defaultTasks[5]
            });

            foreach (var elem in defaultFavorites) context.Favorites.Add(elem);



            IList<Activity> defaultActivity = new List<Activity>();

            defaultActivity.Add(new Activity()
            {
                Guid = "asd10",
                Comment = "Co sie dzieje",
                State = State.Started,
                User = defaultUsers[0],
                Task = defaultTasks[0],
                Group = defaultGroup[1]
            });
            defaultActivity.Add(new Activity()
            {
                Guid = "asd11",
                Comment = "Co sie bedzie dziac",
                State = State.Paused,
                User = defaultUsers[0],
                Task = defaultTasks[0],
                Group = defaultGroup[1]
            });
            defaultActivity.Add(new Activity()
            {
                Guid = "asd12",
                Comment = "Co sie stalo",
                State = State.Stoped,
                User = defaultUsers[0],
                Task = defaultTasks[0],
                Group = defaultGroup[1]
            });
            defaultActivity.Add(new Activity()
            {
                Guid = "asd1",
                Comment = "activity testowy 1",
                State = State.Paused,
                User = defaultUsers[1],
                Task = defaultTasks[2],
                Group = defaultGroup[2]
            });
            defaultActivity.Add(new Activity()
            {
                Guid = "asd2",
                Comment = "activity testowy 2",
                State = State.Paused,
                User = defaultUsers[2],
                Task = defaultTasks[1],
                Group = defaultGroup[2]
            });
            defaultActivity.Add(new Activity()
            {
                Guid = "asd3",
                Comment = "activity testowy 3",
                State = State.Paused,
                User = defaultUsers[3],
                Task = defaultTasks[3],
                Group = defaultGroup[2]
            });

            foreach (var elem in defaultActivity) context.Activity.Add(elem);



            IList<PartsOfActivity> defaultPartsOfActivity = new List<PartsOfActivity>();

            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[0],
                Start = new DateTime(2017, 5, 25, 11, 30, 10),
                Stop = new DateTime(2017, 5, 25, 12, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 30, 10),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[0],
                Start = new DateTime(2017, 5, 25, 12, 30, 10),
                Stop = new DateTime(2017, 5, 25, 13, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 2, 10),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[1],
                Start = new DateTime(2017, 5, 25, 13, 30, 10),
                Stop = new DateTime(2017, 5, 25, 14, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 5, 40),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[1],
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new DateTime(2000, 1, 1, 6, 30, 10),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[2],
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new DateTime(2000, 1, 2, 0, 30, 0),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[2],
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 0, 1),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[3],
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 0, 1),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[4],
                Start = new DateTime(2017, 5, 23, 10, 30, 10),
                Stop = new DateTime(2017, 5, 23, 11, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 0, 1),
            });
            defaultPartsOfActivity.Add(new PartsOfActivity()
            {
                Activity = defaultActivity[5],
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 15, 11, 30, 10),
                Duration = new DateTime(2000, 1, 1, 0, 0, 1),
            });

            foreach (var elem in defaultPartsOfActivity) context.PartsOfActivity.Add(elem);

            base.Seed(context);
        }


    }
}