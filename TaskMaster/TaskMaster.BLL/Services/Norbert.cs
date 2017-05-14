using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class Norbert
    {
        public ActivityRepositories ActivityRepositories = new ActivityRepositories();
        public GroupRepositories GroupRepositories = new GroupRepositories();
        public UserRepositories UserRepositories = new UserRepositories();

        public bool IsEmailInBase(string email)
        {
            var userlist = UserRepositories.GetAll();
            return userlist.Any(var => var.Email.Equals(email));
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

        public List<ActivityDto> ActivitiesFromTimeToTime(DateTime start, DateTime stop)
        {
            var activitylist = new List<ActivityDto>();
            var actlist = ActivityRepositories.GetAll();
            foreach (var act in actlist)
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