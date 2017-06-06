using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserRepositories : RepoBase<User>, IUserRepositories
    {
        public void Add(UserDto dto)
        {
            base.Add(Mapper.Map<User>(dto));
        }

        public void Delete(UserDto dto)
        {
            base.Delete(Mapper.Map<User>(dto));
        }

        public new IList<UserDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<UserDto>).ToList();
        }

        public new UserDto Get(int id)
        {
            return Mapper.Map<UserDto>(base.Get(id));
        }

        public UserDto Get(string email)
        {
            return GetAll().FirstOrDefault(v => v.Email.Equals(email));
        }

        public void Edit(UserDto dto)
        {
            base.Edit(Mapper.Map<User>(dto),"UserId");
        }
    }
}