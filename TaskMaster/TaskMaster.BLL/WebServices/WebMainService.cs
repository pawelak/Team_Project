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
            var activityList = _mainService.ActivitiesFromMonthAgo(email);
            var resultList = new List<ActivityViewModel>();
            var sum = new TimeSpan();
            long max = 0;

            foreach (var act in activityList)
            {
                sum = TimeSpan.Zero;
                sum = act.PartsOfActivity.Aggregate(sum, (current, p) => current + p.Duration);
                if (sum.Ticks > max)
                {
                    max = sum.Ticks;
                }
            }
            foreach (var act in activityList)
            {
                sum = TimeSpan.Zero;
                sum = act.PartsOfActivity.Aggregate(sum, (current, p) => current + p.Duration);
                double pom = (int)((double) sum.Ticks / max * 100);
                var activityViewModel = new ActivityViewModel
                {
                    Name = act.Task.Name,
                    Second = sum.Seconds,
                    Minute = sum.Minutes,
                    Hour = sum.Hours,
                    Time = sum,
                    Percent =  pom+"%"
                };
                resultList.Add(activityViewModel);
            }
            
            return resultList;
        }

    }
}
