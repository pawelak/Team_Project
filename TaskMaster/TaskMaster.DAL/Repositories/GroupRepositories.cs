using System.Collections.Generic;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class GroupRepositories : RepoBase<Group>, IGroupRepositories
    {
        public GroupRepositories()
        {
            Mapper.Initialize(ctg => ctg.AddProfile(new MapperProfil()));
        }
        public void Add(GroupDto dto)
        {
            base.Add(Mapper.Map<Group>(dto));
        }

        public void Delete(GroupDto dto)
        {
            base.Delete(Mapper.Map<Group>(dto));
        }

        public IList<GroupDto> GetAll()
        {
            IList<GroupDto> list = new List<GroupDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<GroupDto>(VARIABLE));
            }
            return list;
        }

        public new GroupDto Get(int ID)
        {
            return Mapper.Map<GroupDto>(base.Get(ID));
        }

        public void Edit(GroupDto dto)
        {
            base.Edit(Mapper.Map<Group>(dto));
        }
    }
}