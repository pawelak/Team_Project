using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
    class ActivityWebApiService
    {
        readonly private UserRepositories _userRepositories = new UserRepositories();
        public List<ActivityMobileDto> ActivitiesFromTimeToTime(string email, DateTime start, DateTime stop)
        {
            var activityList = new List<ActivityDto>();
            var user = _userRepositories.Get(email);
            foreach (var act in user.Activity)
            {
                activityList.AddRange(act.PartsOfActivity.Where(a => a.Start.CompareTo(start) > 0)
                    .Where(a => a.Start.CompareTo(stop) < 0)
                    .Where(a => a.Stop.CompareTo(start) > 0)
                    .Where(a => a.Stop.CompareTo(stop) < 0)
                    .Select(a => act));
            }
            var returnList = new List<ActivityMobileDto>();
            foreach (var act in activityList)
            {
                ActivityMobileDto tmp = new ActivityMobileDto()
                {
                    UserEmail = user.Email,
                    Comment = act.Comment,
                    GroupName = act.Group.NameGroup,
                    Guid = act.Guid,
                    TaskName = act.Task.Name,
                    Token = null,
                    EditState = act.EditState,
                    State = act.State,
                    TaskPartsList = act.PartsOfActivity

                };

            }

            return returnList;
        }
    }
}
