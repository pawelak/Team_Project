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
        
        


        //machlojona baza
        private DataSimulation _database = new DataSimulation();


        public List<ActivityMobileDto> GetActivityFromLastWeek(string email)
        {
            var dt7daysAgo = DateTime.Now.AddDays(-7);
            var activityRawList = new List<ActivityDto>();

            //------------machlojenia z bazą----------------
            //var user = _database.userDtosList.First(a => a.Email.Equals(email));
            var user = _userRepositories.Get(email);



            foreach (var act in user.Activity)
            {
                if (act.State != State.Planned)
                {
                    activityRawList.AddRange(act.PartsOfActivity
                    .Where(a => (a.Stop > dt7daysAgo) && (a.Start < DateTime.Now))
                    .Select(a => act));
                }
              
            }
            
            
            var returnedList = new List<ActivityMobileDto>();

            foreach (var raw in activityRawList)
            {
                var tmpListOfPatrs = new List<PartsOfActivityMobileDto>();
                foreach (var part in raw.PartsOfActivity)
                {
                    
                    tmpListOfPatrs.Add(new PartsOfActivityMobileDto
                    {

                        Start = part.Start.ToString("HH:mm:ss dd/MM/yyyy",CultureInfo.InvariantCulture),
                        Stop = part.Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Duration = part.Duration.ToString("G", CultureInfo.InvariantCulture)
                    });
                }

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

            var tmpListOfParts = new List<PartsOfActivityDto> ();
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

            // do dopisania jak baza ruszy (sprawdzania czy są w bazie i robienie nowych 
            var tmpGroup = new GroupDto();
            var tmpTask = new TaskDto();
            var tmpUser = new UserDto();

            
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

            _database.activityDtoList.Add(tmpActivity);

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
