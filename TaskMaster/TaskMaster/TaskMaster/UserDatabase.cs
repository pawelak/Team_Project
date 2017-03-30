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
        readonly SQLiteConnection _database;
        public UserDatabase (string dbpath)
        {
            _database = new SQLiteConnection(dbpath);
            _database.CreateTable<Activities>();
            _database.CreateTable<Favorites>();
            _database.CreateTable<PartsOfActivity>();
            _database.CreateTable<Tasks>();
            _database.CreateTable<User>();
        }
        
        public int SaveUser(User user)
        {
            return _database.Insert(user);
        }

        public int SaveTask(Task task)
        {
            return _database.Insert(task);
        }
    }
}
