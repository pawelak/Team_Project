using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class GroupRepositories : RepoBase<Group>, IGroupRepositories
    {
        public GroupRepositories()
        {
            
        }
        public void Add(GroupDto dto)
        {
            Group entity = dto.ToGroup();
            base.Add(entity);
        }

        public void Delete(GroupDto dto)
        {
            Group entity = dto.ToGroup();
            base.Delete(entity);
        }

        public IList<GroupDto> GetAll()
        {
            IList<GroupDto> list = new List<GroupDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new GroupDto(VARIABLE));
            }
            return list;
        }

        public GroupDto Get(int ID)
        {
            return new GroupDto(base.Get(ID));
        }

        public void Edit(GroupDto dto)
        {
            Group entity = dto.ToGroup();
            base.Edit(entity);
        }
    }
}