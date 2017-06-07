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
            var result = Mapper.Map<PartsOfActivity>(dto);
            base.Add(result);
        }
        public void Attach(PartsOfActivityDto dto)
        {
            var result = Mapper.Map<PartsOfActivity>(dto);
            base.Attach(result);
        }
        public void Delete(PartsOfActivityDto dto)
        {
            var result = Mapper.Map<PartsOfActivity>(dto);
            base.Delete(result);
        }
        public new IList<PartsOfActivityDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<PartsOfActivityDto>);
            return result.ToList();
        }
        public new PartsOfActivityDto Get(int id)
        {
            var result = Mapper.Map<PartsOfActivityDto>(base.Get(id));
            return result;
        }
        public void Edit(PartsOfActivityDto dto)
        {
            var result = Mapper.Map<PartsOfActivity>(dto);
            base.Edit(result, p => p.PartsOfActivityId);
        }
    }
}