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
        private UserDto _loggedUser;
        private UserService()
        {
            
        }

        public static UserService Instance => _instance ?? (_instance = new UserService());

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

        public async Task DeletePartOfActivity(PartsOfActivityDto partsOfActivityDto)
        {
            await Database.DeletePartOfActivity(partsOfActivityDto);
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

        public async Task<bool> IsLoggedUser()
        {
            var user = await Database.GetLoggedUser();
            if (user == null)
            {
                return false;
            }
            if (_loggedUser == null)
            {
                SetLoggedUser(user);
            }
            return true;
        }
        public void SetLoggedUser(UserDto user)
        {
            _loggedUser = user;
        }

        public UserDto GetLoggedUser()
        {
            if (_loggedUser != null)
            {
                return _loggedUser;
            }
            var user = new UserDto
            {
                UserId = -1
            };
            return user;
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

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await Database.GetUser(id);
            return user;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await Database.GetUserByEmail(email);
            return user;
        }

        public async Task LogoutUser()
        {
            await Database.LogoutUser();
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

        public async Task<List<FavoritesDto>> GetUserFavorites(int id)
        {
            var get = await Database.GetUserFavorites(id);
            return get;
        }

        public async Task<FavoritesDto> GetFavoriteByTaskId(int id)
        {
            var get = await Database.GetFavoriteByTaskId(id);
            return get;
        }
        public async Task SaveFavorite(FavoritesDto favoritesDto)
        {
            await Database.SaveFavorite(favoritesDto);
        }

        public async Task DeleteFavorite(FavoritesDto favoritesDto)
        {
            await Database.DeleteFavorite(favoritesDto);
        }
    }
}
