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
    public class PlannedWebApiService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly GroupWebApiService _groupWebApiService = new GroupWebApiService();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();
        private readonly PartsOfActivityRepositories _partsOfActivityRepositories = new PartsOfActivityRepositories();



        public List<PlannedMobileDto> GetPlanned(string email)
        {
            var user = _userRepositories.Get(email);

            var activityRawList = user.Activities.Where(act => act.State == State.Planned).ToList();

            var returnedList = new List<PlannedMobileDto>();

            foreach (var raw in activityRawList)
            {
                var tmpListOfPatrs = raw.PartsOfActivity.Select(part => new PartsOfActivityMobileDto
                {
                    Start = part.Start.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = part.Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = part.Duration.ToString("G", CultureInfo.InvariantCulture)
                }).ToList();

                var tmp = new PlannedMobileDto()
                {
                    UserEmail = raw.User.Email,
                    Comment = raw.Comment,
                    Guid = raw.Guid,
                    TaskName = raw.Task.Name,
                    Token = null,
                    EditState = raw.EditState,
                    State = raw.State,
                    TaskPart = tmpListOfPatrs.First()
                };
                returnedList.Add(tmp);
            }

            return returnedList;
        }

        public bool AddPlanned(PlannedMobileDto plannActivityMobileDto)
        {
            if (plannActivityMobileDto.State != State.Planned) return false;

            var idAct = 0;
            var tmpTask = _taskRepositories.Get(plannActivityMobileDto.TaskName);
            if (tmpTask == null)
            {
                tmpTask = new TaskDto()
                {
                    Description = "",
                    Name = plannActivityMobileDto.TaskName,
                };
                _taskRepositories.Add(tmpTask);
            }
            var tmpActivity = new ActivityDto
            {
                Comment = plannActivityMobileDto.Comment,
                Guid = plannActivityMobileDto.Guid,
                State = plannActivityMobileDto.State,
                EditState = plannActivityMobileDto.EditState,
                User = _userRepositories.Get(plannActivityMobileDto.UserEmail),
                Task = _taskRepositories.Get(plannActivityMobileDto.TaskName),
                Group = _groupRepositories.Get(1)
            };
            try
            {
                idAct = _activityRepositories.Add(tmpActivity);
            }
            catch (Exception e)
            {
                return false;
            }
            var tmpPart = new PartsOfActivityDto
            {
                Start = DateTime.ParseExact(plannActivityMobileDto.TaskPart.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                Stop = DateTime.ParseExact(plannActivityMobileDto.TaskPart.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                Duration = TimeSpan.ParseExact(plannActivityMobileDto.TaskPart.Duration, "G", CultureInfo.InvariantCulture),
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
            return false;

        }



        //tu edit jeszcze

    }
}