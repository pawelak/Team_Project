using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TaskMaster.Models;
using System;

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

        public async Task<int> SaveActivity(Activities activity)
        {
            if (activity.ActivityId != 0)
            {
                var resultUpdate = await _database.UpdateAsync(activity);
                return resultUpdate;
            }
            await _database.InsertAsync(activity);
            var resultInsert = await _database.ExecuteScalarAsync<int>("Select activityId From Activities Order By activityId Desc Limit 1");
            return resultInsert;
        }
        public async Task<int> SaveFavorite(Favorites favorite)
        {
            if (favorite.FavoriteId != 0)
            {
                var resultUpdate = await _database.UpdateAsync(favorite);
                return resultUpdate;
            }
            await _database.InsertAsync(favorite);
            var resultInsert = await _database.ExecuteScalarAsync<int>("Select favoriteId From Favorites Order By favoriteId Desc Limit 1");
            return resultInsert;
        }
        public async Task<int> SavePartOfTask(PartsOfActivity part)
        {
            if (part.PartId != 0)
            {
                var resultUpdate = await _database.UpdateAsync(part);
                return resultUpdate;
            }
            await _database.InsertAsync(part);
            var resultInsert = await _database.ExecuteScalarAsync<int>("Select partId From PartsOfActivity Order By partId Desc Limit 1");
            return resultInsert;
        }
        public async Task<int> SaveUser(User user)
        {
            if (user.UserId != 0)
            {
                var resultUpdate = await _database.UpdateAsync(user);
                return resultUpdate;
            }
            await _database.InsertAsync(user);
            var resultInsert = await _database.ExecuteScalarAsync<int>("Select userId From User Order By userId Desc Limit 1");
            return resultInsert;
        }
        public async Task<int> SaveTask(Tasks task)
        {
            if (task.TaskId != 0)
            {
                var resultUpdate = await _database.UpdateAsync(task);
                return resultUpdate;
            }
            await _database.InsertAsync(task);
            var resultInsert = await _database.ExecuteScalarAsync<int>("Select taskId FROM Tasks ORDER BY taskId DESC LIMIT 1");
            return resultInsert;
        }
        public async Task<Tasks> GetTask(Tasks task)
        {
            var result = await _database.Table<Tasks>().Where(t => t.Name == task.Name).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<PartsOfActivity>> GetPartsList()
        {
            var result = await _database.Table<PartsOfActivity>().ToListAsync();
            return result;
        }
        public async Task<Activities> GetActivity(int id)
        {
            var result = await _database.Table<Activities>().Where(t => t.ActivityId == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Tasks> GetTaskById(int id)
        {
            var result = await _database.Table<Tasks>().Where(t => t.TaskId == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<Activities>> GetActivitiesByStatus(StatusType status)
        {
            var result = await _database.Table<Activities>().Where(t => t.Status == status).ToListAsync();
            if (result == null)
                return await Task.FromResult<List<Activities>>(null);
            return result;
        }
        public async Task<PartsOfActivity> GetLastActivityPart(int id)
        {
            var result = await _database.Table<PartsOfActivity>()
                .Where(t => t.ActivityId == id)
                .OrderByDescending(t => t.PartId)
                .FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<List<PartsOfActivity>> GetPartsOfActivityByStatus(DateTime Start,int id)
        {

            var result = await _database.Table<PartsOfActivity>().Where(t => t.ActivityId == id && Convert.ToDateTime(t.Start).Day == Start.Day && Convert.ToDateTime(t.Start).Month == Start.Month && Convert.ToDateTime(t.Start).Year == Start.Year).ToListAsync();
            return result;
        }
    }
}
