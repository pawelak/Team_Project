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
    public class WebHistEditService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly PartsOfActivityRepositories _partsOfActivityRepositories = new PartsOfActivityRepositories();

        public List<List<string>> ShowEditTask(string email, int id)
        {
            var user = _userRepositories.Get(email);
            var listOfAct = new List<ActivityDto>();

            var userAct = user.Activities.FirstOrDefault(i => i.ActivityId.Equals(id));
         
            List<List<string>> retruned = new List<List<string>>();
            List<string> list = new List<string>();

            list.Add(userAct.Task.Name);
            list.Add(userAct.Task.Description);

            retruned.Add(list);

            foreach (var part in userAct.PartsOfActivity)
            {
                List<string> tmp = new List<string>();
                
                tmp.Add(part.PartsOfActivityId.ToString());
                tmp.Add(part.Start.ToString());
                tmp.Add(part.Stop.ToString()); 
                tmp.Add(part.Duration.ToString());    
                
                retruned.Add(tmp);   
            }



            return retruned;
        }

        public bool EditPart(List<string> newList)
        {
            try
            {
                TimeSpan tmp = Convert.ToDateTime(newList[2]) - Convert.ToDateTime(newList[1]);

                PartsOfActivityDto part = new PartsOfActivityDto()
                {
                    PartsOfActivityId = Convert.ToInt32(newList[0]),
                    Start = Convert.ToDateTime(newList[1]),
                    Stop = Convert.ToDateTime(newList[2]),
                    Duration = tmp

                };

                _partsOfActivityRepositories.Edit(part);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPart(List<string> newList)
        {
            try
            {
                TimeSpan tmp = Convert.ToDateTime(newList[2]) - Convert.ToDateTime(newList[1]);

                PartsOfActivityDto part = new PartsOfActivityDto()
                {
                    PartsOfActivityId = Convert.ToInt32(newList[0]),
                    Start = Convert.ToDateTime(newList[1]),
                    Stop = Convert.ToDateTime(newList[2]),
                    Duration = tmp

                };

                _partsOfActivityRepositories.Add(part);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
