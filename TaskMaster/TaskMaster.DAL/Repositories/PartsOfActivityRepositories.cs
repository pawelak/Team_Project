using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    // TODO uwagi z klasy ActivityRepositories obowiazuja rowniez tutaj
    public class PartsOfActivityRepositories : RepoBase<PartsOfActivity>, IPartsOfActivityRepositories
    {
        public void Add(PartsOfActivityDto dto)
        {
            var result = Mapper.Map<PartsOfActivity>(dto);
            result.ActivityId = result.Activity.ActivityId;
            result.Activity = null;
            base.Add(result);
        }
        public void Delete(PartsOfActivityDto dto)
        {
            var obj = Mapper.Map<PartsOfActivity>(dto);
            var result = Db.PartsOfActivity.Find(obj.PartsOfActivityId);
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
            var obj = Mapper.Map<PartsOfActivity>(dto);
            var result = Db.PartsOfActivity.Find(obj.PartsOfActivityId);
            result.ActivityId = obj.ActivityId;
            result.Duration = obj.Duration;
            result.Start = obj.Start;
            result.Stop = obj.Stop;
            base.Edit(result);
        }
    }
}