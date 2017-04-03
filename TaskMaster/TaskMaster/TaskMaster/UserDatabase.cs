﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Android.Support.V7.View.Menu;
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
            _database.InsertAsync(activity);
            return _database.ExecuteScalarAsync<int>("Select activityId From Activities Order By activityId Desc Limit 1");

        }
        public Task<int> SaveFavorite(Favorites favorite)
        {
            if (favorite.favoriteId != 0)
                return _database.UpdateAsync(favorite);
            _database.InsertAsync(favorite);
            return _database.ExecuteScalarAsync<int>("Select favoriteId From Favorites Order By favoriteId Desc Limit 1");
        }
        public Task<int> SavePartOfTask(PartsOfActivity part)
        {
            if (part.partId != 0)
                return _database.UpdateAsync(part);
            _database.InsertAsync(part);
            return _database.ExecuteScalarAsync<int>("Select partId From PartsOfActivity Order By partId Desc Limit 1");
        }
        public Task<int> SaveUser(User user)
        {
            if (user.userId != 0)
                return _database.UpdateAsync(user);
            _database.InsertAsync(user);
            return _database.ExecuteScalarAsync<int>("Select userId From User Order By userId Desc Limit 1");
        }
        public Task<int> SaveTask(Tasks task)
        {
            if (task.taskId != 0)
                return _database.UpdateAsync(task);
            _database.InsertAsync(task);
            return _database.ExecuteScalarAsync<int>("Select taskid FROM Tasks ORDER BY taskId DESC LIMIT 1");
        }

        public Task<Tasks> GetTask(Tasks task)
        {
            return _database.Table<Tasks>().Where(t => t.name == task.name).FirstOrDefaultAsync();
        }

        public Task<List<PartsOfActivity>> GetPartsList()
        {
            return _database.Table<PartsOfActivity>().ToListAsync();
        }

        public Task<Activities> GetActivity(int id)
        {
            return _database.Table<Activities>().Where(t => t.activityId == id).FirstOrDefaultAsync();
        }

        public Task<Tasks> GetTaskById(int id)
        {
            return _database.Table<Tasks>().Where(t => t.taskId == id).FirstOrDefaultAsync();
        }
    }
}
