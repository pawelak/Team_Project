using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.WebModels;

namespace TaskMaster.BLL.WebServices
{
    public class WebMainService
    {
        private readonly MainService _mainService = new MainService();

        public List<ActivityViewModel> ShowActivity(string email)//, DateTime start, DateTime stop) FIX
        {
            var activityList = _mainService.ActivitiesFromTimeToTime(email);//, start, stop);FIX

            var resultList = new List<ActivityViewModel>();
            foreach (var a in activityList)
            {
                var sum = new TimeSpan();
                sum = a.PartsOfActivity.Aggregate(sum, (current, p) => current + p.Duration);
                var activityViewModel = new ActivityViewModel
                {
                    Name = a.Task.Name,
                    Time = sum.TotalSeconds
                };
                resultList.Add(activityViewModel);

            }
            return resultList;
        }

    }
}
