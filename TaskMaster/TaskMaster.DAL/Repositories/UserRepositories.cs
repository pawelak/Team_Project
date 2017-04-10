using System.Collections.Generic;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserRepositories : RepoBase<User>, IUserRepositories
    {
        public UserRepositories()
        {
            Mapper.Initialize(ctg => ctg.AddProfile(new MapperProfil()));
        }
        public void Add(UserDto dto)
        {
            base.Add(Mapper.Map<User>(dto));
        }

        public void Delete(UserDto dto)
        {
            base.Delete(Mapper.Map<User>(dto));
        }

        public IList<UserDto> GetAll()
        {
            IList<UserDto> list = new List<UserDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<UserDto>(VARIABLE));
            }
            return list;
        }

        public new UserDto Get(int ID)
        {
            return Mapper.Map<UserDto>(base.Get(ID));
        }

        public void Edit(UserDto dto)
        {
            base.Edit(Mapper.Map<User>(dto));
        }
    }
}