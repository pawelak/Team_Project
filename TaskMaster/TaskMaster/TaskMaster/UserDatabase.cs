using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TaskMaster.Models;

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

        public Task<int> SaveActivity(Activities activity)
        {
            if (activity.activityId != 0)
                return _database.UpdateAsync(activity);
            return _database.InsertAsync(activity);
        }
        public Task<int> SaveFavorite(Favorites favorite)
        {
            if (favorite.favoriteId != 0)
                return _database.UpdateAsync(favorite);
            return _database.InsertAsync(favorite);
        }
        public Task<int> SavePartOfTask(PartsOfActivity part)
        {
            if (part.id != 0)
                return _database.UpdateAsync(part);
            return _database.InsertAsync(part);
        }
        public Task<int> SaveUser(User user)
        {
            if (user.userId != 0)
                return _database.UpdateAsync(user);
            return _database.InsertAsync(user);
        }
        public Task<int> SaveTask(Tasks task)
        {
            if (task.taskId != 0)
                return _database.UpdateAsync(task);
            return _database.InsertAsync(task);          
        }
    }
}
