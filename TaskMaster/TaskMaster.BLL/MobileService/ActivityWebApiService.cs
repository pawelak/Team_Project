using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
    public class ActivityWebApiService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly GroupWebApiService _groupWebApiService = new GroupWebApiService();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();


        //machlojona baza
        private DataSimulation _database = new DataSimulation();


        public List<ActivityMobileDto> GetActivityFromLastWeek(string email)
        {
            var date7DaysAgo = DateTime.Now.AddDays(-7);
            var user = _userRepositories.Get(email);
            var activityRawList = new List<ActivityDto>();

            foreach (var act in user.Activity)
            {
                if (act.State != State.Planned)
                {
                    activityRawList.AddRange(act.PartsOfActivity
                    .Where(a => (a.Stop > date7DaysAgo) && (a.Start < DateTime.Now))
                    .Select(a => act));
                }
            }

            var returnedList = new List<ActivityMobileDto>();

            foreach (var raw in activityRawList)
            {
                var tmpListOfPatrs = raw.PartsOfActivity.Select(part => new PartsOfActivityMobileDto
                {
                    Start = part.Start.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = part.Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = part.Duration.ToString("G", CultureInfo.InvariantCulture)
                }).ToList();

                var tmp = new ActivityMobileDto
                {
                    UserEmail = raw.User.Email,
                    Comment = raw.Comment,
                    Guid = raw.Guid,
                    TaskName = raw.Task.Name,
                    Token = null,
                    EditState = raw.EditState,
                    State = raw.State,
                    TaskPartsList = tmpListOfPatrs

                };
                returnedList.Add(tmp);
            }

            return returnedList;
        }


        public bool AddActivity(ActivityMobileDto activityMobileDto)
        {
            //veryfikacje i walidacje dorzucić 

            var tmpListOfParts = new List<PartsOfActivityDto>();
            foreach (var part in activityMobileDto.TaskPartsList)
            {

                var tmpPart = new PartsOfActivityDto
                {
                    Start = DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = TimeSpan.ParseExact(part.Duration, "G", CultureInfo.InvariantCulture)
                };
                tmpListOfParts.Add(tmpPart);
                
            }

            var tmpGroup = _groupRepositories.Get(1);
            tmpGroup.Activity = null;
            tmpGroup.UserGroup = null;
            var tmpTask = _taskRepositories.Get(activityMobileDto.TaskName);
            tmpTask.Activity = null;
            tmpTask.Favorites = null;


            if (tmpTask == null)
            {
                tmpTask = new TaskDto()
                {
                    Description = "",
                    Name = activityMobileDto.TaskName
                };
            }
            var tmpUser = _userRepositories.Get(activityMobileDto.UserEmail);
            tmpUser.Activity = null;
            tmpUser.Favorites = null;
            tmpUser.Tokens = null;
            tmpUser.UserGroup = null;

            var tmpActivity = new ActivityDto
            {
                Comment = activityMobileDto.Comment,
                Guid = activityMobileDto.Guid,
                State = activityMobileDto.State,
                EditState = activityMobileDto.EditState,
                Group = tmpGroup,
                PartsOfActivity = tmpListOfParts,
                Task = tmpTask,
                User = tmpUser
            };
            try
            {
                _activityRepositories.Add(tmpActivity);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        public List<PartsOfActivityMobileDto> test()
        {
            var old = _database.partsOfActivityDtosList;
            var outList = new List<PartsOfActivityMobileDto>();
            foreach (var tmp in old)
            {
                outList.Add(new PartsOfActivityMobileDto
                {
                    Start = tmp.Start.ToString(CultureInfo.InvariantCulture),
                    Stop = tmp.Stop.ToString(CultureInfo.InvariantCulture)
                   
                });
            }

            return outList;
        }

        public string test2()
        {
            var dt7daysAgo = DateTime.Now.AddDays(-7);
            var activityRawList = new List<ActivityDto>();

            //------------machlojenia z bazą----------------
            //var user = _userRepositories.Get(email);
            var user = _database.userDtosList.First(a => a.Email.Equals("a@a.pl"));


            foreach (var act in user.Activity)
            {
                activityRawList.AddRange(act.PartsOfActivity
                    .Where(a => (a.Stop > dt7daysAgo ) && (a.Start < DateTime.Now))
                    .Select(a => act));
            }

            return activityRawList.Count().ToString();
        }

    }


}
