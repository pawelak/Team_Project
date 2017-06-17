using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.WebServices
{
    public struct dataTime
    {
        public string name;
        public DateTime data;
        public string durationTime;
        public string workTime;
        public string breakTime;

    };

    public class WebCalService
    {
       

        private readonly UserRepositories _userRepositories = new UserRepositories();
        public List<List<string>> LastMonth(string email, int days)
        {

            var date7DaysAgo = DateTime.Now.AddDays(-days);
            var user = _userRepositories.Get(email);
            var listOfAct = new List<ActivityDto>();

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

        public List<List<string>> Calendar(string email, int year, int month)
        {


     
            var user = _userRepositories.Get(email);
            var listOfAct = new List<ActivityDto>();

            var tabUserAct = user.Activities;
            var notPlaned = tabUserAct.Where(a => a.State != State.Planned);


            foreach (var cos in notPlaned)
            {
                foreach (var part in cos.PartsOfActivity)
                {
                    if ((part.Stop.Year == year) && (part.Stop.Month == month))
                    {
                        listOfAct.Add(cos);
                        break;
                    }
                }
            }


            List<dataTime> returnedList = new List<dataTime>();
            foreach (var act in listOfAct)
            {
                DateTime stopData = new DateTime(1999, 1, 1, 1, 1, 1);
                TimeSpan sum = new TimeSpan();
                DateTime min = new DateTime(2200, 1, 1, 1, 1, 1);
                DateTime max = new DateTime(1900, 1, 1, 1, 1, 1);

                foreach (var pact in act.PartsOfActivity)
                {
                    if (stopData < pact.Stop)
                    {
                        stopData = pact.Stop;
                    }
                    if (pact.Start < min)
                    {
                        min = pact.Start;
                    }
                    if (pact.Stop > max)
                    {
                        max = pact.Stop;
                    }
                    sum += pact.Duration;

                }

                dataTime tmp = new dataTime()
                {
                    name = act.Task.Name,
                    data = stopData,
                    durationTime = (max-min).ToString(),
                    workTime = sum.ToString(),
                    breakTime = ((max-min)-sum).ToString()
                };

                returnedList.Add(tmp);
            }

            var returned = new List<List<string>>();

            foreach (var tmp1 in returnedList)
            {
                var tmp2 = new List<string>();

                tmp2.Add(tmp1.name);
                tmp2.Add(tmp1.data.ToString());
                tmp2.Add(tmp1.durationTime);
                tmp2.Add(tmp1.workTime);
                tmp2.Add(tmp1.breakTime);

                returned.Add(tmp2);
            }


            return returned ;
        }
    }
}
