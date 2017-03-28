using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TaskMaster.Models;

namespace TaskMaster
{
    class UserDatabase
    {
        readonly SQLiteAsyncConnection database;
        public UserDatabase (string dbpath)
        {
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTablesAsync<User, Events, PartsOfTasks, Tasks>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return database.Table<User>().ToListAsync();
        }
    }
}
