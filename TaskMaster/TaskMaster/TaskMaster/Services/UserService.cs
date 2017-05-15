using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMaster.ModelsDto;
using Xamarin.Forms;

namespace TaskMaster.Services
{
    public class UserService
    {
        private static UserService _instance;
        private static UserDatabase _database;
        private static UserDatabase Database => _database ?? (_database = new UserDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("UserSQLite.db3")));

        private UserService()
        {
            
        }

        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }
        public async Task<int> SaveActivity(ActivitiesDto activitiesDto)
        {
            if (activitiesDto.ActivityId != 0)
            {
                var update = await Database.UpdateActivity(activitiesDto);
                return update;
            }
            var insert = await Database.InsertActivity(activitiesDto);
            return insert;
        }

        public async Task<int> SaveFavorites(FavoritesDto favoritesDto)
        {
            if (favoritesDto.FavoriteId != 0)
            {
                var update = await Database.UpdateFavorites(favoritesDto);
                return update;
            }
            var insert = await Database.InsertFavorite(favoritesDto);
            return insert;
        }

        public async Task<int> SavePartOfActivity(PartsOfActivityDto partsOfActivityDto)
        {
            if (partsOfActivityDto.PartId != 0)
            {
                var update = await Database.UpdatePartOfActivity(partsOfActivityDto);
                return update;
            }
            var insert = await Database.InsertPartOfActivity(partsOfActivityDto);
            return insert;
        }

        public async Task<int> SaveTask(TasksDto tasksDto)
        {
            if (tasksDto.TaskId != 0)
            {
                var update = await Database.UpdateTask(tasksDto);
                return update;
            }
            var insert = await Database.InsertTask(tasksDto);
            return insert;
        }

        public async Task<int> SaveUser(UserDto userDto)
        {
            if (userDto.UserId != 0)
            {
                var update = await Database.UpdateUser(userDto);
                return update;
            }
            var insert = await Database.InsertUser(userDto);
            return insert;
        }

        public async Task<TasksDto> GetTask(TasksDto tasksDto)
        {
            var update = await Database.GetTask(tasksDto);
            return update;
        }

        public async Task<TasksDto> GetTaskById(int id)
        {
            var update = await Database.GetTaskById(id);
            return update;
        }

        public async Task<List<PartsOfActivityDto>> GetPartsOfActivityList()
        {
            var update = await Database.GetPartsList();
            return update;
        }

        public async Task<ActivitiesDto> GetActivity(int id)
        {
            var update = await Database.GetActivity(id);
            return update;
        }

        public async Task<List<ActivitiesDto>> GetActivitiesByStatus(StatusType status)
        {
            var update = await Database.GetActivitiesByStatus(status);
            return update;
        }

        public async Task<PartsOfActivityDto> GetLastActivityPart(int id)
        {
            var update = await Database.GetLastActivityPart(id);
            return update;
        }

        public async Task<List<PartsOfActivityDto>> GetPartsOfActivityByActivityId(int id)
        {
            var update = await Database.GetPartsOfActivityByActivityId(id);
            return update;
        }
        public async Task<PartsOfActivityDto> GetPartsOfActivityById(int id)
        {
            var update = await Database.GetPartsOfActivityById(id);
            return update;
        }
    }
}
