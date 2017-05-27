using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    /*
    przekonwertowac aktivites w main services

    przekonwertowac timespan na datatime w entity i przemapowac na time span w mapperze

    dorobic rekorfdow anjlepiej od pawelka

    serwis dla historii oraz serwis dla home paga
    */


    public class MainService
    {
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();

        public List<ActivityDto> ActivitiesFromTimeToTime(string email) //, DateTime start, DateTime stop) FIX
        {
            var activityList = new List<ActivityDto>();
            var user = _userRepositories.Get(email);
            return (List<ActivityDto>) user.Activity; //DEL
            //foreach (var act in user.Activity)
            //{
            //    activityList.AddRange(act.PartsOfActivity.Where(a => a.Start.CompareTo(start) > 0)
            //        .Where(a => a.Start.CompareTo(stop) < 0)
            //        .Where(a => a.Stop.CompareTo(start) > 0)
            //        .Where(a => a.Stop.CompareTo(stop) < 0)
            //        .Select(a => act));
            //}
            //return activityList;
        }


    }

}