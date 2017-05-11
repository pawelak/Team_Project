using System.Collections.Generic;
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

        public IList<PartsOfActivityDto> GetAll()
        {
            IList<PartsOfActivityDto> list = new List<PartsOfActivityDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<PartsOfActivityDto>(VARIABLE));
            }
            return list;
        }

        public new PartsOfActivityDto Get(int ID)
        {
            return Mapper.Map<PartsOfActivityDto>(base.Get(ID));
        }

        public void Edit(PartsOfActivityDto dto)
        {
            base.Edit(Mapper.Map<PartsOfActivity>(dto));
        }
    }
}