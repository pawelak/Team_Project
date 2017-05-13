using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class DiffrentCOdes
    {
        public ActivityRepositories ActivityRepositories = new ActivityRepositories();
        public GroupRepositories GroupRepositories = new GroupRepositories();
        public UserRepositories UserRepositories = new UserRepositories();
   
        public bool IsEmailInBase(string email)
        {
            var userlist = UserRepositories.GetAll();
            return userlist.Any(u => u.Email.Equals(email));
        }

        public bool IsNameInBase(string name)
        {
            var userlist = UserRepositories.GetAll();
            return userlist.Any(u => u.Name.Equals(name));
        }

        public List<UserDto> UsersInGroup(string groupname)
        {
            var grouplist = GroupRepositories.GetAll().FirstOrDefault(g => g.NameGroup.Equals(groupname));
            var userlist = grouplist.UserGroup.Select(u => u.User).ToList();
            return userlist;
        }

        public bool Authorization(string login, string password)
        {
            UserDto user = UserRepositories.Get(login);
            if (user.Equals(UserRepositories.GetAll().f)) return false;
            return false;
        }


        public List<ActivityDto> ActivitiesFromTimeToTime(string login, DateTime start, DateTime stop) // dontwork
        {
            var activitylist = new List<ActivityDto>();

            var zmiena = new UserDto();
            foreach (var VARIABLE in UserRepositories.GetAll())
            {
                if (VARIABLE.Email.Equals(login)) zmiena = VARIABLE;
            }


            foreach (var act in zmiena.Activity)
            {
                activitylist.AddRange(act.PartsOfActivity.Where(a => a.Start.CompareTo(start) > 0)
                    .Where(a => a.Start.CompareTo(stop) < 0)
                    .Where(a => a.Stop.CompareTo(start) > 0)
                    .Where(a => a.Stop.CompareTo(stop) < 0)
                    .Select(a => act));
            }
            return activitylist;
        }
    }

}