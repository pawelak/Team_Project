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
            base.Add(Mapper.Map<Group>(dto));
        }

        public void Delete(GroupDto dto)
        {
            base.Delete(Mapper.Map<Group>(dto));
        }

        public new IList<GroupDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<GroupDto>).ToList();
        }

        public new GroupDto Get(int id)
        {
            return Mapper.Map<GroupDto>(base.Get(id));
        }

        public GroupDto Get(string nameGroup)
        {
            return GetAll().FirstOrDefault(v => v.NameGroup.Equals(nameGroup));
        }

        public void Edit(GroupDto dto)
        {
            var map = Mapper.Map<Group>(dto);
            base.Edit(map);
        }
    }
}