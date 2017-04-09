using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserGroupRepositories : RepoBase<UserGroup>, IUserGroupRepositories
    {
        public UserGroupRepositories()
        {
            
        }
        public void Add(UserGroupDto dto)
        {
            UserGroup entity = dto.ToUserGroup();
            base.Add(entity);
        }

        public void Delete(UserGroupDto dto)
        {
            UserGroup entity = dto.ToUserGroup();
            base.Delete(entity);
        }

        public IList<UserGroupDto> GetAll()
        {
            IList<UserGroupDto> list = new List<UserGroupDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new UserGroupDto(VARIABLE));
            }
            return list;
        }

        public UserGroupDto Get(int ID)
        {
            return new UserGroupDto(base.Get(ID));
        }

        public void Edit(UserGroupDto dto)
        {
            UserGroup entity = dto.ToUserGroup();
            base.Edit(entity);
        }
    }
}