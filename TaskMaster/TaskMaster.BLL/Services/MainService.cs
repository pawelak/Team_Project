using System.Collections.Generic;
using System.Linq;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class MainService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();

        public List<ActivityDto> ActivitiesFromMonthAgo(string email) 
        {
            var result = new List<ActivityDto>();
            var activityList = _userRepositories.Get(email).Activity;
            
            //TODO sprzed miesiaca wyjmowanie czasu

            result = activityList.ToList();
            return result;
        }
    }
}