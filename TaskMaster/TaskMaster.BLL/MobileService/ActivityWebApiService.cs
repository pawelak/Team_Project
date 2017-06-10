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
            if (activityMobileDto.State == State.Planned) return false;
            var idAct = 0;
            var tmpTask = _taskRepositories.Get(activityMobileDto.TaskName);
            if (tmpTask == null)
            {
                tmpTask = new TaskDto()
                {
                    Description = "",
                    Name = activityMobileDto.TaskName
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
            catch (Exception e)
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
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
            


    }
}
