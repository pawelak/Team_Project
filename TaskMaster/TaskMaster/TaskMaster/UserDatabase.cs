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
        public UserDatabase(string dbpath)
        {
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<Users>().Wait();
        }

        public Task<List<Users>> GetUserAsync()
        {
            return database.Table<Users>().ToListAsync();
        }
    }
}
