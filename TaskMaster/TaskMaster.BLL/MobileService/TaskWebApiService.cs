using System.Collections.Generic;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
    public class TaskWebApiService
    {
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();

        public TasksMobileDto GetTask(string name)
        {
            var tmp = _taskRepositories.Get(name);
            if (tmp != null)
            {
                var tmpTaskMobile = new TasksMobileDto()
                {
                    Name = tmp.Name,
                    Type = tmp.Type
                };
                return tmpTaskMobile;
            }
            return null;
        }

        public bool AddTask(TasksMobileDto tasksMobileDto)
        {
            if (_taskRepositories.Get(tasksMobileDto.Name) != null)
            {
                return false;
            }
            var tmp = new TaskDto()
            {
                Name = tasksMobileDto.Name,
                Type = tasksMobileDto.Type,
                Activities = new List<ActivityDto>(),
                Description =  "",
                Favorites = new List<FavoritesDto>()
            };
            _taskRepositories.Add(tmp);
            return true;
        }
    }
}