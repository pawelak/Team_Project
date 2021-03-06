﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserRepositories : RepoBase<User>, IUserRepositories
    {
        public int Add(UserDto dto)
        {
            var result = Mapper.Map<User>(dto);
            result.Activities = null;
            result.Tokens = null;
            result.Favorites = null;
            result.UserGroup = null;
            base.Add(result);
            return result.UserId;
        }
        public void Delete(UserDto dto)
        {
            var obj = Mapper.Map<User>(dto);
            var result = Db.User.Find(obj.UserId);
            base.Delete(result);
        }
        public new IList<UserDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<UserDto>);
            return result.ToList();
        }
        public new UserDto Get(int id)
        {
            var result = Mapper.Map<UserDto>(base.Get(id));
            return result;
        }
        public UserDto Get(string email)
        {
            var result = GetAll().FirstOrDefault(v => v.Email.Equals(email));
            return result;
        }
        public void Edit(UserDto dto)
        {
            var obj = Mapper.Map<User>(dto);
            var result = Db.User.Find(obj.UserId);
            result.Description = obj.Description;
            result.Email = obj.Email;
            result.Name = obj.Name;
            base.Edit(result);
        }
    }
}