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
        readonly private UserRepositories _userRepositories = new UserRepositories();


        //machlojona baza
        private DataSimulation database = new DataSimulation();


        public List<ActivityMobileDto> GetActivityFromLastWeek(string email)
        {
            var dt7daysAgo = DateTime.Now.AddDays(-7);
            var activityRawList = new List<ActivityDto>();

            //------------machlojenia z bazą----------------
            var user = database.userDtosList.First(a => a.Email.Equals(email));
            //var user = _userRepositories.Get(email);



            foreach (var act in user.Activity)
            {
                activityRawList.AddRange(act.PartsOfActivity
                     .Where(a => (a.Stop > dt7daysAgo) && (a.Start < DateTime.Now))
                     .Select(a => act));
            }

            var returnedList = new List<ActivityMobileDto>();

            foreach (var raw in activityRawList)
            {
                var tmpListOfPatrs = new List<PartsOfActivityMobileDto>();
                foreach (var part in raw.PartsOfActivity)
                {
                    tmpListOfPatrs.Add(new PartsOfActivityMobileDto()
                    {
                        Start = part.Start.ToString(CultureInfo.InvariantCulture),
                        Stop = part.Stop.ToString(CultureInfo.InvariantCulture),
                        Duration = part.Duration.ToString(CultureInfo.InvariantCulture)
                    });
                }

                var tmp = new ActivityMobileDto()
                {
                    UserEmail = raw.User.Email,
                    Comment = raw.Comment,
                    GroupName = raw.Group.NameGroup,
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

        public List<PartsOfActivityMobileDto> test()
        {
            var old = database.partsOfActivityDtosList;
            var outList = new List<PartsOfActivityMobileDto>();
            foreach (var tmp in old)
            {
                outList.Add(new PartsOfActivityMobileDto()
                {
                    Start = tmp.Start.ToString(CultureInfo.InvariantCulture),
                    Stop = tmp.Stop.ToString(CultureInfo.InvariantCulture),
                    Duration = tmp.Duration.ToString(CultureInfo.InvariantCulture)
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
            var user = database.userDtosList.First(a => a.Email.Equals("a@a.pl"));


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
