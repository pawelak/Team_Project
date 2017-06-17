using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.WebModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.WebServices
{
    public class WebMainService
    {
        private readonly MainService _mainService = new MainService();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly UserRepositories _userRepositories = new UserRepositories();

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

        public List<List<string>> History(string email)
        {
          
            var user = _userRepositories.Get(email);
            var listOfAct = new List<DAL.DTOModels.ActivityDto>();
            var result = new List<List<string>>();
            
            foreach (var act in user.Activities)
            {
                TimeSpan sum = new TimeSpan();
                DateTime min = new DateTime(2200,1,1,1,1,1); 
                DateTime max = new DateTime(1900, 1, 1, 1, 1, 1);

                foreach (var parts in act.PartsOfActivity)
                {
                    if (parts.Start < min)
                    {
                        min = parts.Start;
                    }
                    if (parts.Stop > max)
                    {
                        max = parts.Stop;
                    }
                    sum += parts.Duration;
                }
                var help = new List<string>();

                help.Add(act.Task.Name);
                help.Add(min.ToShortDateString());
                help.Add((max-min).ToString());
                help.Add(sum.ToString());
                help.Add(((max-min)-sum).ToString());

                help.Add(act.ActivityId.ToString());
                help.Add(act.Task.TaskId.ToString()); 

                result.Add(help);
            }
           
            return result;
        }

     

    }
}
