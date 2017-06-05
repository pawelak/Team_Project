using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserGroupRepositories : RepoBase<UserGroup>, IUserGroupRepositories
    {
        public void Add(UserGroupDto dto)
        {
            base.Add(Mapper.Map<UserGroup>(dto));
        }

        public void Delete(UserGroupDto dto)
        {
            base.Delete(Mapper.Map<UserGroup>(dto));
        }

        public new IList<UserGroupDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<UserGroupDto>).ToList();
        }

        public new UserGroupDto Get(int id)
        {
            return Mapper.Map<UserGroupDto>(base.Get(id));
        }

        public IList<UserGroupDto> Get(string email)
        {
            var list = GetAll();
            return list.Where(l => l.User.Email.Equals(email)).ToList();
        }

        public void Edit(UserGroupDto dto)
        {
            base.Edit(Mapper.Map<UserGroup>(dto));
        }
    }
}