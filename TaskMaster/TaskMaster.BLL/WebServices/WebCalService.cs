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
    class WebCalService
    {
        internal struct dataTime
        {
            private string name;
            private DateTime data;
            private string czasdurationTime;
            private string workTime;
            private string breakTime;

        };

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

        public List<dataTime> Calendar(string email, DateTime a)
        {
                






            return null;
        }
    }
}
