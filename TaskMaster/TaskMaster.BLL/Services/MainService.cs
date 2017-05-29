using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    /*TODO

    serwis dla historii oraz serwis dla home paga
    */


    public class MainService
    {
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();

        public List<ActivityDto> ActivitiesFromTimeToTime(string email) //TODO - frow time to time
        {
            var result = new List<ActivityDto>();
            var activityList = _activityRepositories.GetAll();
            var user = _userRepositories.GetAll();
            foreach (var act in activityList)
            {
                if(act.User.Email.Equals(email)) result.Add(act);
            }
            result = activityList.ToList();
            return result;
        }


    }

}