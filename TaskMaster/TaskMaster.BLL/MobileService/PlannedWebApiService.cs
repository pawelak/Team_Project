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
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();
        private readonly PartsOfActivityRepositories _partsOfActivityRepositories = new PartsOfActivityRepositories();



        public List<PlannedMobileDto> GetPlanned(string email)
        {
            var user = _userRepositories.Get(email);
            var activityRawList = user.Activities.Where(act => act.State == State.Planned).ToList();
            var returnedList = new List<PlannedMobileDto>();

            foreach (var rawActivity in activityRawList)
            {
                var tmpListOfPatrs = rawActivity.PartsOfActivity.Select(part => new PartsOfActivityMobileDto
                {
                    Start = part.Start.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = part.Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = part.Duration.ToString("G", CultureInfo.InvariantCulture)
                }).ToList();

                try
                {
                    var tmp = new PlannedMobileDto()
                    {
                        UserEmail = rawActivity.User.Email,
                        Comment = rawActivity.Comment,
                        Guid = rawActivity.Guid,
                        TaskName = rawActivity.Task.Name,
                        Token = null,
                        EditState = rawActivity.EditState,
                        State = rawActivity.State,
                        TaskPart = tmpListOfPatrs.First()
                    };
                    returnedList.Add(tmp);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return returnedList;
        }

        public bool AddPlanned(PlannedMobileDto plannedMobileDto)
        {
            if (plannedMobileDto.State != State.Planned) return false;
            TaskDto tmpTask = null;
            int idAct;
            try
            {
                tmpTask = _taskRepositories.Get(plannedMobileDto.TaskName);
            }
            catch (Exception e)
            {
            }

            if (tmpTask == null)
            {
                tmpTask = new TaskDto()
                {
                    Description = "",
                    Name = plannedMobileDto.TaskName,
                };
                _taskRepositories.Add(tmpTask);
            }
            var tmpActivity = new ActivityDto
            {
                Comment = plannedMobileDto.Comment,
                Guid = plannedMobileDto.Guid,
                State = plannedMobileDto.State,
                EditState = plannedMobileDto.EditState,
                User = _userRepositories.Get(plannedMobileDto.UserEmail),
                Task = _taskRepositories.Get(plannedMobileDto.TaskName),
                Group = _groupRepositories.Get(1)
            };
            try
            {
                idAct = _activityRepositories.Add(tmpActivity);
            }
            catch (Exception)
            {
                return false;
            }
            
            try
            {
                var tmpPart = new PartsOfActivityDto
                {
                    Start = DateTime.ParseExact(plannedMobileDto.TaskPart.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop =  DateTime.MaxValue,
                    Duration = TimeSpan.Zero,
                    Activity = _activityRepositories.Get(idAct)
                };
                _partsOfActivityRepositories.Add(tmpPart);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        public bool EndPlanned(PlannedMobileDto plannedMobileDto)
        {
            var user = _userRepositories.Get(plannedMobileDto.UserEmail);
            var planned = user.Activities.First(g => g.Guid.Equals(plannedMobileDto.Guid));
            planned.EditState = EditState.Delete;
            _activityRepositories.Edit(planned);
            return true;

        }

        public bool Delete(string guid, string email)
        {
            var toDel = _activityRepositories.Get(email).First(g => g.Guid.Equals(guid));
            try
            {
                _activityRepositories.Delete(toDel);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



    }
}



//var tmpPart = new PartsOfActivityDto
//{
//    Start = DateTime.ParseExact(plannedMobileDto.TaskPart.Start, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
//    Stop = new DateTime(),
//    Duration = new TimeSpan(),
//    Activity = _activityRepositories.Get(idAct)
//};