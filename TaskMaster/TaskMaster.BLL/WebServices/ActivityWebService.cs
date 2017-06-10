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
    public class ActivityWebService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();

        public List<ActivityDto> getActivitiesFromPeriod(string email,int days)
        {
            days = 30;
            var date7DaysAgo = DateTime.Now.AddDays(-days);
            var user = _userRepositories.Get(email);
            var activityRawList = new List<DAL.DTOModels.ActivityDto>();

            foreach (var act in user.Activities)
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

            foreach (var list in returnedList)
            {
                int sum;
                foreach (var part in list.TaskPartsList)
                {
                 //   sum += part.Duration;
                }    
            }

            return returnedList;

        }

        public List<List<string>> LastMonth(string email, int days)
        {
            days = 10;
            var date7DaysAgo = DateTime.Now.AddDays(-days);
            var user = _userRepositories.Get(email);
            var listOfAct = new List<DAL.DTOModels.ActivityDto>();

            var result = new List<List<string>>();

            foreach(var act in user.Activities)
            {
                if (act.State != State.Planned)
                {
                    listOfAct.AddRange(act.PartsOfActivity
                        .Where(a => (a.Stop > date7DaysAgo) && (a.Start < DateTime.Now))
                        .Select(a => act));
                }
            }

            foreach (var act in listOfAct)
            {
               
                TimeSpan sum = new TimeSpan();
                foreach (var part in act.PartsOfActivity)
                {
                    sum += part.Duration;
                }
                List<string> help = new List<string>();
              
                help.Add(act.Task.Name);
                help.Add(sum.ToString());
                result.Add(help);
            }

            return result;
        }

        
        public List<List<string>> LongestTask(string email)
        {
            //var user = _userRepositories.Get(email);
            // var listOfAct = user.Activities;

            // var result = new List<List<string>>();

            // foreach (var act in listOfAct)
            // {

            //     TimeSpan sum = new TimeSpan();
            //     foreach (var part in act.PartsOfActivity)
            //     {
            //         sum += part.Duration;
            //     }
            //     List<string> help = new List<string>();

            //     help.Add(act.Task.Name);
            //     help.Add(sum.ToString());

            //     result.Add(help);
            // }

            // return result;

            var user = _userRepositories.Get(email);
            var listOfAct = new List<DAL.DTOModels.ActivityDto>();
            var result = new List<List<string>>();
            int r;
            int tmpMaks = Int32.MinValue;

            foreach (var act in user.Activities)
            {
                TimeSpan sum = new TimeSpan();
                TimeSpan max2 = new TimeSpan(Int32.MinValue);
          
                foreach (var parts in act.PartsOfActivity)
                {
                    sum += parts.Duration;
                }

                var help = new List<string>();

                help.Add(act.Task.Name);
                help.Add(sum.ToString());
                
                result.Add(help);
           }

           foreach (var maks in result)
           {

               r = Int32.Parse(maks[1]);
                
               if ( r > tmpMaks )
               {
                   
               }

           }
            
            return result;
        }
    }
}
