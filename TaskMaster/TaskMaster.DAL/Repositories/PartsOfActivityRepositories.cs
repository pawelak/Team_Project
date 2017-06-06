using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class PartsOfActivityRepositories : RepoBase<PartsOfActivity>, IPartsOfActivityRepositories
    {
        public void Add(PartsOfActivityDto dto)
        {
            base.Add(Mapper.Map<PartsOfActivity>(dto));
        }

        public void Delete(PartsOfActivityDto dto)
        {
            base.Delete(Mapper.Map<PartsOfActivity>(dto));
        }

        public new IList<PartsOfActivityDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<PartsOfActivityDto>).ToList();
        }

        public new PartsOfActivityDto Get(int id)
        {
            return Mapper.Map<PartsOfActivityDto>(base.Get(id));
        }

        public void Edit(PartsOfActivityDto dto)
        {
            base.Edit(Mapper.Map<PartsOfActivity>(dto),"PartsOfActivityId");
        }
    }
}