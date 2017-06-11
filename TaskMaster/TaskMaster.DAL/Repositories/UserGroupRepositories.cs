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
            var result = Mapper.Map<UserGroup>(dto);
            result.UserId = result.User.UserId;
            result.User = null;
            result.GroupId = result.Group.GroupId;
            result.Group = null;
            base.Add(result);
        }
        public void Delete(UserGroupDto dto)
        {
            var obj = Mapper.Map<UserGroup>(dto);
            var result = Db.UserGroup.Find(obj.UserGroupId);
            base.Delete(result);
        }
        public new IList<UserGroupDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<UserGroupDto>);
            return result.ToList();
        }
        public new UserGroupDto Get(int id)
        {
            var result = Mapper.Map<UserGroupDto>(base.Get(id));
            return result;
        }
        public IList<UserGroupDto> Get(string email)
        {
            var result = GetAll().Where(l => l.User.Email.Equals(email));
            return result.ToList();
        }
        public void Edit(UserGroupDto dto)
        {
            var obj = Mapper.Map<UserGroup>(dto);
            var result = Db.UserGroup.Find(obj.UserGroupId);
            result.GroupId = obj.GroupId;
            result.UserId = obj.UserId;
            base.Edit(result);
        }
    }
}