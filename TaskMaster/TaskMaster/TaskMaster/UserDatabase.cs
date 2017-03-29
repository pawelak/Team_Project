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
            _database.CreateTablesAsync<Events,PartsOfTasks,Tasks,User>().Wait();
           
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> SaveUser(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<int> SaveTask(Task task)
        {
            return _database.InsertAsync(task);
        }
    }
}
