using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class GroupRepositories : RepoBase<Group>, IGroupRepositories
    {
        public void Add(GroupDto dto)
        {
            var result = Mapper.Map<Group>(dto);
            base.Add(result);
        }
        public void Attach(GroupDto dto)
        {
            var result = Mapper.Map<Group>(dto);
            base.Attach(result);
        }
        public void Delete(GroupDto dto)
        {
            var result = Mapper.Map<Group>(dto);
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
            var result = Mapper.Map<Group>(dto);
            base.Edit(result, p => p.GroupId);
        }
    }
}