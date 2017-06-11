using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public struct LongTask
    {
        public string name;
        public TimeSpan dur;
        public double totalDur;
    }


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

            var tabUserAct = user.Activities;
            var notPlaned = tabUserAct.Where(a => a.State != State.Planned);
           

            foreach (var cos in notPlaned)
            {
                foreach (var part in cos.PartsOfActivity)
                {
                    if ((part.Stop > date7DaysAgo) && (part.Start < DateTime.Now))
                    {
                        listOfAct.Add(cos);
                        break;
                    }
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
            var list = new List<LongTask>();

            var user = _userRepositories.Get(email);


            foreach (var act in user.Activities)
            {
                TimeSpan sum = new TimeSpan();
               

                foreach (var parts in act.PartsOfActivity)
                {
                    sum += parts.Duration;
                }
                LongTask strLongTask =new LongTask();
                strLongTask.dur = sum;
                strLongTask.name = act.Task.Name;
                
                list.Add(strLongTask);
               
            }

            var orderList = list.OrderByDescending(x => x.dur).ToList();
            var max = orderList[0].dur.TotalSeconds;

            var returned = new List<List<string>>();

            foreach (var item in orderList)
            {
                    

                var tmp = item.dur.ToString();
                    var tmpPerc = (item.dur.TotalSeconds / max)*100;
                    var tmp3 = Math.Round(tmpPerc, 0);
                var cos = new List<string>();

                cos.Add(tmp);
                cos.Add(item.name);
                    cos.Add(tmp3.ToString());

                returned.Add(cos);
               
            }

            return returned;
        }

        public List<LongTask> ActivityFromBase(string email)
        {
            //------------------Geslerowa---------------------------
            
            var Period = DateTime.Now.AddDays(-30);
            var user = _userRepositories.Get(email);
            var listOfAct = new List<DAL.DTOModels.ActivityDto>();
            var tabUserAct = user.Activities;

            var notPlaned = tabUserAct.Where(a => a.State != State.Planned);


            foreach (var cos in notPlaned)
            {
                foreach (var part in cos.PartsOfActivity)
                {
                    if ((part.Stop > Period) && (part.Start < DateTime.Now))
                    {
                        listOfAct.Add(cos);
                        break;
                    }
                }
            }
            //--------------------------------------------------------


            var list = new List<LongTask>();

            


            foreach (var act in user.Activities)
            {
                    TimeSpan sum = new TimeSpan();


                    foreach (var parts in act.PartsOfActivity)
                    {
                        sum += parts.Duration;
                    }
                    LongTask strLongTask = new LongTask();
                    strLongTask.dur = sum;
                    strLongTask.name = act.Task.Name;

                    list.Add(strLongTask);

            }

            var list2 = new List<LongTask>();

          

            foreach (var element in list)
            {
                    var tmpName = element.name;
                    var tmpDur = TimeSpan.Zero;
                    var obi = new LongTask()
                    {
                        dur = tmpDur,
                        name = tmpName
                    };

                    if (!list2.Any(a => a.name.Equals(tmpName)))
                    {
                        list2.Add(obi);
                    }
            }


            var list3 = new List<LongTask>();


            foreach (var el2 in list2)
            {
                    var tmpName = el2.name;
                    var tmpDur = el2.dur;
                    var obi2 = new LongTask()
                    {
                        dur = tmpDur,
                        name = tmpName
                    };

                
                    foreach (var element in list)
                    {
                  
                        if (el2.name.Equals(element.name))
                        {
                            tmpDur += element.dur;
                        }
                    
                    }

                    LongTask strLongTask = new LongTask();
                    strLongTask.name = tmpName;
                    strLongTask.dur = tmpDur;
                    strLongTask.totalDur = Math.Round(tmpDur.TotalSeconds, 0);
                    list3.Add(strLongTask);
                    

            }


            


            return list3;
        }

        public List<List<string>> Last12MOfWork(string email)
        {
            var Period = DateTime.Now.AddMonths(-12);
            var user = _userRepositories.Get(email);
            var listOfAct = new List<DAL.DTOModels.ActivityDto>();
            var tabUserAct = user.Activities;

            var notPlaned = tabUserAct.Where(a => a.State != State.Planned);


            foreach (var cos in notPlaned)
            {
                foreach (var part in cos.PartsOfActivity)
                {
                    if ((part.Stop > Period) && (part.Start < DateTime.Now))
                    {
                        listOfAct.Add(cos);
                        break;
                    }
                }
            }

            var result = new List<List<string>>();
            foreach (var act in listOfAct)
            {


                TimeSpan sum = new TimeSpan();
                foreach (var part in act.PartsOfActivity)
                {
                    sum += part.Duration;
                }

                List<string> help = new List<string>();

                help.Add(act.Task.Name);
                help.Add(Math.Round(sum.TotalMinutes,0).ToString());

                result.Add(help);

            }



            return result;
         }

    }
}
