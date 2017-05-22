using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class MainService
    {
        private readonly ActivityRepositories _activityRepositories = new ActivityRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();
   
        public bool IsEmailInBase(string email)
        {
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Email.Equals(email));
        }

        public bool IsNameInBase(string name)
        {
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Name.Equals(name));
        }

        public List<UserDto> UsersInGroup(string groupName)
        {
            var groupList = _groupRepositories.GetAll().FirstOrDefault(g => g.NameGroup.Equals(groupName));
            var userList = groupList.UserGroup.Select(u => u.User).ToList();
            return userList;
        }

        public bool Authorization(string login, string password) //TODO
        {
            //UserDto user = _userRepositories.Get(login);
            //if (user.Equals(_userRepositories.GetAll().f)) return false;
            return false;
        }

        public void Stworz(string email, string login="A", string password="B")
        {
            UserDto user = new UserDto();
            user.Email = email;
            user.Name = login;
            TokensDto token = new TokensDto();
            token.Token = password;
            //user.Tokens.Add(token);
            _userRepositories.Add(user);
        }

        public List<ActivityDto> ActivitiesFromTimeToTime(string login, DateTime start, DateTime stop) // TODO
        {
            var activitylist = new List<ActivityDto>();

            var zmiena = new UserDto();
            foreach (var VARIABLE in _userRepositories.GetAll())
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