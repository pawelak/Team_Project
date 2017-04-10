using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TaskMaster.Models;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
    public class UserServices
    {
        public async Task<int> SaveActivity(ActivitiesDto activitiesDto)
        {
            if (activitiesDto.ActivityId != 0)
            {
                var result = await App.Database.UpdateActivity(activitiesDto);
                return result;
            }
            var result2 = await App.Database.InsertActivity(activitiesDto);
            return result2;
        }

        public async Task<int> SaveFavorites(FavoritesDto favoritesDto)
        {
            if (favoritesDto.FavoriteId != 0)
            {
                var result = await App.Database.UpdateFavorites(favoritesDto);
                return result;
            }
            var result2 = await App.Database.InsertFavorite(favoritesDto);
            return result2;
        }

        public async Task<int> SavePartOfActivity(PartsOfActivityDto partsOfActivityDto)
        {
            if (partsOfActivityDto.PartId != 0)
            {
                var result = await App.Database.UpdatePartOfActivity(partsOfActivityDto);
                return result;
            }
            var result2 = await App.Database.InsertPartOfActivity(partsOfActivityDto);
            return result2;
        }

        public async Task<int> SaveTask(TasksDto tasksDto)
        {
            if (tasksDto.TaskId != 0)
            {
                var result = await App.Database.UpdateTask(tasksDto);
                return result;
            }
            var result2 = await App.Database.InsertTask(tasksDto);
            return result2;
        }

        public async Task<int> SaveUser(UserDto userDto)
        {
            if (userDto.UserId != 0)
            {
                var result = await App.Database.UpdateUser(userDto);
                return result;
            }
            var result2 = await App.Database.InsertUser(userDto);
            return result2;
        }

        public async Task<TasksDto> GetTask(TasksDto tasksDto)
        {
            var result = await App.Database.GetTask(tasksDto);
            return result;
        }

        public async Task<TasksDto> GetTaskById(int id)
        {
            var result = await App.Database.GetTaskById(id);
            return result;
        }

        public async Task<List<PartsOfActivityDto>> GetPartsOfActivityList()
        {
            var result = await App.Database.GetPartsList();
            return result;
        }

        public async Task<ActivitiesDto> GetActivity(int id)
        {
            var result = await App.Database.GetActivity(id);
            return result;
        }

        public async Task<List<ActivitiesDto>> GetActivitiesByStatus(StatusType status)
        {
            var result = await App.Database.GetActivitiesByStatus(status);
            return result;
        }

        public async Task<PartsOfActivityDto> GetLastActivityPart(int id)
        {
            var result = await App.Database.GetLastActivityPart(id);
            return result;
        }

        public async Task<List<PartsOfActivityDto>> GetPartsOfActivityByActivityId(int id)
        {
            var result = await App.Database.GetPartsOfActivityByActivityId(id);
            return result;
        }
    }
}
