using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.WebModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.WebServices
{
    public class WebMainService
    {
        private readonly MainService _mainService = new MainService();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();

        public List<ActivityViewModel> ShowActivity(string email)//, DateTime start, DateTime stop) FIX
        {
            var activityList = _mainService.ActivitiesFromTimeToTime(email);//, start, stop);FIX
            var resultList = new List<ActivityViewModel>();
            foreach (var a in activityList)
            {
                var sum = new TimeSpan();
                var name="";
                
                var nameList = _taskRepositories.GetAll();
                foreach (var n in nameList)
                {
                    foreach (var v in n.Activity)
                    {
                        if (v.ActivityId == a.ActivityId) name = n.Name;
                    }
                }
                sum = a.PartsOfActivity.Aggregate(sum, (current, p) => current + p.Duration);
                var activityViewModel = new ActivityViewModel
                {
                    Name = name,
                    Second = sum.Seconds,
                    Minute = sum.Minutes,
                    Hour = sum.Hours,
                    Time = sum.ToString("g")
                };
                resultList.Add(activityViewModel);

            }
            return resultList;
        }

    }
}
