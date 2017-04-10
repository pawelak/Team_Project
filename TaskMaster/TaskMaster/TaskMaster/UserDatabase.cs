using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TaskMaster.Models;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public UserDatabase (string dbpath)
        {
            _database = new SQLiteAsyncConnection(dbpath);
            _database.DropTableAsync<Activities>();
            _database.DropTableAsync<Favorites>();
            _database.DropTableAsync<PartsOfActivity>();
            _database.DropTableAsync<Tasks>();
            _database.DropTableAsync<User>();
            _database.CreateTablesAsync<Activities, Favorites, PartsOfActivity, Tasks, User>().Wait();
        }

        public async Task<int> UpdateActivity(ActivitiesDto activitiesDto)
        {
            var activity = AutoMapper.Mapper.Map<Activities>(activitiesDto);
            await _database.UpdateAsync(activity);
            return activity.ActivityId;
        }

        public async Task<int> InsertActivity(ActivitiesDto activityDto)
        {
            var activity = AutoMapper.Mapper.Map<Activities>(activityDto);
            await _database.InsertAsync(activity);
            return activity.ActivityId;
        }

        public async Task<int> UpdateFavorites(FavoritesDto favoritesDto)
        {
            var favorite = AutoMapper.Mapper.Map<Favorites>(favoritesDto);
            await _database.UpdateAsync(favorite);
            return favorite.FavoriteId;
        }

        public async Task<int> InsertFavorite(FavoritesDto favoritesDto)
        {
            var favorite = AutoMapper.Mapper.Map<Favorites>(favoritesDto);
            await _database.InsertAsync(favorite);
            return favorite.FavoriteId;
        }

        public async Task<int> InsertPartOfActivity(PartsOfActivityDto partsOfActivityDto)
        {
            var partOfActivity = AutoMapper.Mapper.Map<PartsOfActivity>(partsOfActivityDto);
            await _database.InsertAsync(partOfActivity);
            return partOfActivity.PartId;
        }

        public async Task<int> UpdatePartOfActivity(PartsOfActivityDto partOfActivityDto)
        {
            var partOfActivity = AutoMapper.Mapper.Map<PartsOfActivity>(partOfActivityDto);
            await _database.UpdateAsync(partOfActivity);
            return partOfActivity.PartId;
        }

        public async Task<int> InsertUser(UserDto userDto)
        {
            var user = AutoMapper.Mapper.Map<User>(userDto);
            await _database.InsertAsync(user);
            return user.UserId;
        }

        public async Task<int> UpdateUser(UserDto userDto)
        {
            var user = AutoMapper.Mapper.Map<User>(userDto);
            await _database.UpdateAsync(user);
            return user.UserId;
        }

        public async Task<int> InsertTask(TasksDto tasksDto)
        {
            var task = AutoMapper.Mapper.Map<Tasks>(tasksDto);
            await _database.InsertAsync(task);
            return task.TaskId;
        }

        public async Task<int> UpdateTask(TasksDto taskDto)
        {
            var task = AutoMapper.Mapper.Map<Tasks>(taskDto);
            await _database.UpdateAsync(task);           
            return task.TaskId;
        }

        public async Task<TasksDto> GetTask(TasksDto taskDto)
        {
            var task = AutoMapper.Mapper.Map<Tasks>(taskDto);
            var result = await _database.Table<Tasks>().Where(t => t.Name == task.Name).FirstOrDefaultAsync();
            taskDto = AutoMapper.Mapper.Map<TasksDto>(result);
            return taskDto;
        }

        public async Task<List<PartsOfActivityDto>> GetPartsList()
        {
            var result = await _database.Table<PartsOfActivity>().ToListAsync();
            var list = AutoMapper.Mapper.Map <List<PartsOfActivityDto>>(result);
            return list;
        }

        public async Task<ActivitiesDto> GetActivity(int id)
        {
            var result = await _database.Table<Activities>().Where(t => t.ActivityId == id).FirstOrDefaultAsync();
            var activity = AutoMapper.Mapper.Map<ActivitiesDto>(result);
            return activity;
        }

        public async Task<TasksDto> GetTaskById(int id)
        {
            var result = await _database.Table<Tasks>().Where(t => t.TaskId == id).FirstOrDefaultAsync();
            var task = AutoMapper.Mapper.Map<TasksDto>(result);
            return task;
        }

        public async Task<List<ActivitiesDto>> GetActivitiesByStatus(StatusType status)
        {
            var result = await _database.Table<Activities>().Where(t => t.Status == status).ToListAsync();
            var list = AutoMapper.Mapper.Map<List<ActivitiesDto>>(result);
            return list;
        }

        public async Task<PartsOfActivityDto> GetLastActivityPart(int id)
        {
            var result = await _database.Table<PartsOfActivity>()
                .Where(t => t.ActivityId == id)
                .OrderByDescending(t => t.PartId)
                .FirstOrDefaultAsync();
            var part = AutoMapper.Mapper.Map<PartsOfActivityDto>(result);
            return part;
        }

        public async Task<List<PartsOfActivityDto>> GetPartsOfActivityByActivityId(int id)
        {
            var result = await _database.Table<PartsOfActivity>().Where(t => t.ActivityId == id).ToListAsync();
            var list = AutoMapper.Mapper.Map<List<PartsOfActivityDto>>(result);
            return list;
        }
    }
}
