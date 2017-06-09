using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    // TODO uwagi z klasy ActivityRepositories obowiazuja rowniez tutaj
    public class GroupRepositories : RepoBase<Group>, IGroupRepositories
    {
        public void Add(GroupDto dto)
        {
            var result = Mapper.Map<Group>(dto);
            result.Activities = null;
            result.UserGroup = null;
            base.Add(result);
        }
        public void Delete(GroupDto dto)
        {
            var obj = Mapper.Map<Group>(dto);
            var result = Db.Group.Find(obj.GroupId);
            base.Delete(result);
        }
        public new IList<GroupDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<GroupDto>);
            return result.ToList();
        }
        public new GroupDto Get(int id)
        {
            var result = Mapper.Map<GroupDto>(base.Get(id));
            return result;
        }
        public GroupDto Get(string nameGroup)
        {
            var result = GetAll().FirstOrDefault(v => v.NameGroup.Equals(nameGroup));
            return result;
        }
        public void Edit(GroupDto dto)
        {
            var obj = Mapper.Map<Group>(dto);
            var result = Db.Group.Find(obj.GroupId);
            result.NameGroup = obj.NameGroup;
            base.Edit(result);
        }
    }
}