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

        public List<ActivityViewModel> ShowActivity(string email)
        {
            var activityList = _mainService.ActivitiesFromTimeToTime(email);
            var resultList = new List<ActivityViewModel>();
            var sum = new TimeSpan();
            var name = "";
            long max = 0;

            foreach (var a in activityList)
            {
                sum = a.PartsOfActivity.Aggregate(sum, (current, p) => current + p.Duration);
                max = sum.Ticks;
            }

            foreach (var a in activityList)
            {
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
                    Time = sum,
                    Percent = sum.Ticks/max
                };
                resultList.Add(activityViewModel);
            }
            
            return resultList;
        }

    }
}
