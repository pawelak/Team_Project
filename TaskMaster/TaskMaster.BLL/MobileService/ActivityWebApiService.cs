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
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();
        private readonly PartsOfActivityRepositories _partsOfActivityRepositories = new PartsOfActivityRepositories();


        public List<ActivityMobileDto> GetActivityFromLastWeek(string email)
        {
            
            var date7DaysAgo = DateTime.Now.AddDays(-7);
            var user = _userRepositories.Get(email);
            var activityRawList = new List<ActivityDto>();

            foreach (var act in user.Activities)
            {
                if (act.State != State.Planned)
                {
                    activityRawList.AddRange(act.PartsOfActivity
                    .Where(a => (a.Stop > date7DaysAgo) && (a.Start < DateTime.Now))
                    .Select(a => act));
                }
            }

            var returnedList = new List<ActivityMobileDto>();

            foreach (var rawActivity in activityRawList)
            {
                var tmpListOfPatrs = rawActivity.PartsOfActivity.Select(part => new PartsOfActivityMobileDto
                {
                    Start = part.Start.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = part.Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = part.Duration.ToString("G", CultureInfo.InvariantCulture)
                }).ToList();

                var tmp = new ActivityMobileDto
                {
                    UserEmail = rawActivity.User.Email,
                    Comment = rawActivity.Comment,
                    Guid = rawActivity.Guid,
                    TaskName = rawActivity.Task.Name,
                    Token = null,
                    EditState = rawActivity.EditState,
                    State = rawActivity.State,
                    TaskPartsList = tmpListOfPatrs

                };
                returnedList.Add(tmp);
            }

            return returnedList;
        }


        public bool AddActivity(ActivityMobileDto activityMobileDto)
        {
            if (activityMobileDto.State == State.Planned) return false;
            TaskDto tmpTask = null;
            int idAct;
            try
            {
               tmpTask = _taskRepositories.Get(activityMobileDto.TaskName);
            }
            catch (Exception e)
            {
            }
            
            if (tmpTask == null)
            {
                tmpTask = new TaskDto()
                {
                    Description = "",
                    Name = activityMobileDto.TaskName,
                    Activities = new List<ActivityDto>(),
                    Favorites = new List<FavoritesDto>(),
                    Type = ""
                    
                };
                _taskRepositories.Add(tmpTask);
            }
            var tmpActivity = new ActivityDto
            {
                Comment = activityMobileDto.Comment,
                Guid = activityMobileDto.Guid,
                State = activityMobileDto.State,
                EditState = activityMobileDto.EditState,
                User = _userRepositories.Get(activityMobileDto.UserEmail),
                Task = _taskRepositories.Get(activityMobileDto.TaskName),
                Group = _groupRepositories.Get(1),
                PartsOfActivity = new List<PartsOfActivityDto>()
            };
            try
            {
                idAct = _activityRepositories.Add(tmpActivity);
            }
            catch (Exception)
            {
                return false;
            }
            foreach (var part in activityMobileDto.TaskPartsList)
            {
                var tmpPart = new PartsOfActivityDto
                {
                    Start = DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = TimeSpan.ParseExact(part.Duration, "G", CultureInfo.InvariantCulture),
                    Activity = _activityRepositories.Get(idAct)
                };
                try
                {
                    _partsOfActivityRepositories.Add(tmpPart);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
            


    }
}
