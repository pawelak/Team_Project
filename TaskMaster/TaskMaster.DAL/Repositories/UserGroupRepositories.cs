using System.Collections.Generic;
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

        public IList<UserGroupDto> GetAll()
        {
            IList<UserGroupDto> list = new List<UserGroupDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<UserGroupDto>(VARIABLE));
            }
            return list;
        }

        public new UserGroupDto Get(int ID)
        {
            return Mapper.Map<UserGroupDto>(base.Get(ID));
        }

        public void Edit(UserGroupDto dto)
        {
            base.Edit(Mapper.Map<UserGroup>(dto));
        }
    }
}