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
            var list = UserRepositories.GetAll();
            return list.Any(var => var.Email.Equals(email));
        }

        public bool IsNameInBase(string name)
        {
            var list = UserRepositories.GetAll();
            return list.Any(var => var.Name.Equals(name));
        }

        public List<UserDto> UsersInGroup(string groupname)
        {
            var group = GroupRepositories.GetAll().FirstOrDefault(var => var.NameGroup.Equals(groupname));
            var list = @group.UserGroup.Select(VARIABLE => VARIABLE.User).ToList();
            return list;
        }

        public List<ActivityDto> FromTime(DateTime start, DateTime stop)
        {
            var list2 = new List<ActivityDto>();
            var list = ActivityRepositories.GetAll();
            foreach (var VAR1 in list)
            {
                list2.AddRange(from VAR2 in VAR1.PartsOfActivity where VAR2.Start.CompareTo(start) > 0 where VAR2.Start.CompareTo(stop) < 0 where VAR2.Stop.CompareTo(start) > 0 where VAR2.Stop.CompareTo(stop) < 0 select VAR1);
            }
            return list2;
        }



    }

}