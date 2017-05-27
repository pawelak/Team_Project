using System;
using System.Collections.Generic;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.MobileService
{
    public class DataSimulation
    {
        public List<ActivityDto> activityDtoList = new List<ActivityDto>();
        public List<FavoritesDto> favoritesDtosList = new List<FavoritesDto>();
        public List<GroupDto> gropDtosList = new List<GroupDto>();
        public List<PartsOfActivityDto> partsOfActivityDtosList = new List<PartsOfActivityDto>();
        public List<TaskDto> taskDtosList = new List<TaskDto>();
        public List<TokensDto> tokensDtosList = new List<TokensDto>();
        public List<UserDto> userDtosList = new List<UserDto>();
        public List<UserGroupDto> userGroupDtosList = new List<UserGroupDto>();


        public DataSimulation()
        {
//activity 
            ActivityDto a1 = new ActivityDto()
            {
                Guid = "asd1",
                Comment = "activity testowy 1",
                EditState = EditState.None,
                State = State.Stoped,
                PartsOfActivity = new List<PartsOfActivityDto>()
            };
            ActivityDto a2 = new ActivityDto()
            {
                Guid = "asd2",
                Comment = "activity testowy 2",
                EditState = EditState.None,
                State = State.Stoped,
                PartsOfActivity = new List<PartsOfActivityDto>()
            };
            ActivityDto a3 = new ActivityDto()
            {
                Guid = "asd3",
                Comment = "activity testowy 3",
                EditState = EditState.None,
                State = State.Stoped,
                PartsOfActivity = new List<PartsOfActivityDto>()
            };



//group
            GroupDto gr1 = new GroupDto()
            {
                NameGroup = "nazwa1",
                UserGroup = new List<UserGroupDto>(),
                Activity = new List<ActivityDto>()
            };
            GroupDto gr2 = new GroupDto()
            {
                NameGroup = "nazwa2",
                 UserGroup = new List<UserGroupDto>(),
                Activity = new List<ActivityDto>()
            };
            GroupDto gr3 = new GroupDto()
            {
                NameGroup = "nazwa3",
                UserGroup = new List<UserGroupDto>(),
                Activity = new List<ActivityDto>()
            };


//PartsOfActivity
            PartsOfActivityDto p1 = new PartsOfActivityDto()
            {
                Start = new DateTime(2017, 5, 25,10,30,10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new TimeSpan(0,0,0,1)
            };
            PartsOfActivityDto p2 = new PartsOfActivityDto()
            {
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new TimeSpan(0, 0, 0, 1)
            };
            PartsOfActivityDto p3 = new PartsOfActivityDto()
            {
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new TimeSpan(0, 0, 0, 1)
            };
            PartsOfActivityDto p4 = new PartsOfActivityDto()
            {
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 25, 11, 30, 10),
                Duration = new TimeSpan(0, 0, 0, 1)
            };
            PartsOfActivityDto p5 = new PartsOfActivityDto()
            {
                Start = new DateTime(2017, 5, 23, 10, 30, 10),
                Stop = new DateTime(2017, 5, 23, 11, 30, 10),
                Duration = new TimeSpan(0, 0, 0, 1)
            };
            PartsOfActivityDto p6 = new PartsOfActivityDto()
            {
                Start = new DateTime(2017, 5, 25, 10, 30, 10),
                Stop = new DateTime(2017, 5, 15, 11, 30, 10),
                Duration = new TimeSpan(0, 0, 0, 1)
            };


//Task
            TaskDto t1 = new TaskDto()
            {
                Name = "biega1",
                Type = "typ1",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>()
            };
            TaskDto t2 = new TaskDto()
            {
                Name = "biega2",
                Type = "typ2",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>()
            };
            TaskDto t3 = new TaskDto()
            {
                Name = "biega3",
                Type = "typ3",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>()
            };
            TaskDto t4 = new TaskDto()
            {
                Name = "biega4",
                Type = "typ4",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>()
            };
            TaskDto t5 = new TaskDto()
            {
                Name = "biega5 do Fav",
                Type = "typ5",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>()
            };
            TaskDto t6 = new TaskDto()
            {
                Name = "biega6 do Fav",
                Type = "typ6",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>()
            };


//token
            TokensDto token1 = new TokensDto()
            {
                Token = "qwe1",
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            };
            TokensDto token2 = new TokensDto()
            {
                Token = "qwe2",
                BrowserType = BrowserType.Firefox,
                PlatformType = PlatformType.Ios
            };
            TokensDto token3 = new TokensDto()
            {
                Token = "qwe3",
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            };
            TokensDto token4 = new TokensDto()
            {
                Token = "qwe4",
                BrowserType = BrowserType.Firefox,
                PlatformType = PlatformType.Ios
            };
            TokensDto token5 = new TokensDto()
            {
                Token = "qwe5",
                BrowserType = BrowserType.Chrome,
                PlatformType = PlatformType.Android
            };
            TokensDto token6 = new TokensDto()
            {
                Token = "qwe6",
                BrowserType = BrowserType.Firefox,
                PlatformType = PlatformType.Ios
            };


//users
            UserDto u1 = new UserDto()
            {
                Email = "a@a.pl",
                Description = "testowy nr 1",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>(),
                UserGroup = new List<UserGroupDto>(),
                Tokens = new List<TokensDto>()

            };
            UserDto u2 = new UserDto()
            {
                Email = "b@b.pl",
                Description = "testowy nr 2",
                Activity = new List<ActivityDto>(),
                Favorites = new List<FavoritesDto>(),
                UserGroup = new List<UserGroupDto>(),
                Tokens = new List<TokensDto>()
            };



//favorites
            FavoritesDto f1 = new FavoritesDto()
            {
                User = u1,
                Task = t1
            };
            FavoritesDto f2 = new FavoritesDto()
            {
                User = u2,
                Task = t5
            };
            FavoritesDto f3 = new FavoritesDto()
            {
                User = u2,
                Task = t6
            };

//UserGroup
            UserGroupDto ug1 = new UserGroupDto()
            {
                User = u1,
                Group = gr1,
            };
            UserGroupDto ug2 = new UserGroupDto()   
            {
                User = u1,
                Group = gr2,
            };
            //UserGroupDto ug3 = new UserGroupDto()   //  ?
            //{
            //    User = u2,
            //    Group = gr1,
            //};

            //build database

            a1.PartsOfActivity.Add(p1);
            a1.User = u1;
            a1.Task = t1;
            a1.Group = gr1;

            a2.PartsOfActivity.Add(p2);
            a2.PartsOfActivity.Add(p3);
            a2.User = u1;
            a2.Task = t2;
            a2.Group = gr2;

            a3.PartsOfActivity.Add(p4);
            a3.PartsOfActivity.Add(p5);
            a3.PartsOfActivity.Add(p6);
            a3.User = u2;
            a3.Task = t3;
            a3.Group = gr3;
            

            gr1.Activity.Add(a1);
            gr1.UserGroup.Add(ug1);
            gr2.Activity.Add(a2);
            gr2.Activity.Add(a3);
            gr2.UserGroup.Add(ug2);


            p1.Activity = a1;
            p2.Activity = a2;
            p3.Activity = a2;
            p4.Activity = a3;
            p5.Activity = a3;
            p6.Activity = a3;


            t1.Activity.Add(a1);
            t2.Activity.Add(a2);
            t3.Activity.Add(a3);


            token1.User = u1;
            token2.User = u2;
            token3.User = u2;


            u1.Activity.Add(a1);
            u1.Favorites.Add(f1);
            u1.Tokens.Add(token1);
            u1.UserGroup.Add(ug1);

            u2.Activity.Add(a2);
            u2.Activity.Add(a3);
            u2.Favorites.Add(f2);
            u2.Favorites.Add(f3);
            u2.Tokens.Add(token2);
            u2.Tokens.Add(token3);
            u2.UserGroup.Add(ug2);


            activityDtoList.Add(a1);
            activityDtoList.Add(a2);
            activityDtoList.Add(a3);
            favoritesDtosList.Add(f1);
            favoritesDtosList.Add(f2);
            favoritesDtosList.Add(f3);
            gropDtosList.Add(gr1);
            gropDtosList.Add(gr2);
            gropDtosList.Add(gr3);
            partsOfActivityDtosList.Add(p1);
            partsOfActivityDtosList.Add(p2);
            partsOfActivityDtosList.Add(p3);
            partsOfActivityDtosList.Add(p4);
            partsOfActivityDtosList.Add(p5);
            partsOfActivityDtosList.Add(p6);
            taskDtosList.Add(t1);
            taskDtosList.Add(t2);
            taskDtosList.Add(t3);
            taskDtosList.Add(t4);
            taskDtosList.Add(t5);
            taskDtosList.Add(t6);
            tokensDtosList.Add(token1);
            tokensDtosList.Add(token3);
            tokensDtosList.Add(token4);
            tokensDtosList.Add(token5);
            userDtosList.Add(u1);
            userDtosList.Add(u2);
            userGroupDtosList.Add(ug1);
            userGroupDtosList.Add(ug2);



        }

       


    }
}