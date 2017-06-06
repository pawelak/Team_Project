using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.BLL.WebModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.WebServices
{
    class ActivityWebService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();

        public List<ActivityDto> getActivitiesFromPeriod(string email,int days)
        {
            days = 7;
            var date7DaysAgo = DateTime.Now.AddDays(-days);
            var user = _userRepositories.Get(email);
            var activityRawList = new List<DAL.DTOModels.ActivityDto>();

            foreach (var act in user.Activity)
            {
                if (act.State != State.Planned)
                {
                    activityRawList.AddRange(act.PartsOfActivity
                        .Where(a => (a.Stop > date7DaysAgo) && (a.Start < DateTime.Now))
                        .Select(a => act));
                }
            }
            var returnedList = new List<ActivityDto>();

            foreach (var raw in activityRawList)
            {
                var tmpListOfPatrs = raw.PartsOfActivity.Select(part => new PartsOfActivityDto
                {
                    Start = part.Start.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = part.Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = part.Duration.ToString("G", CultureInfo.InvariantCulture)
                }).ToList();

                var tmp = new ActivityDto
                {
                    UserEmail = raw.User.Email,
                    Comment = raw.Comment,
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
    }
}
