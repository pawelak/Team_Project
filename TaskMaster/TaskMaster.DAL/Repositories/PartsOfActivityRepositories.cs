using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class PartsOfActivityRepositories : RepoBase<PartsOfActivity>, IPartsOfActivityRepositories
    {
        public PartsOfActivityRepositories()
        {
            
        }
        public void Add(PartsOfActivityDto dto)
        {
            PartsOfActivity entity = dto.ToPartsOfActivity();
            base.Add(entity);
        }

        public void Delete(PartsOfActivityDto dto)
        {
            PartsOfActivity entity = dto.ToPartsOfActivity();
            base.Delete(entity);
        }

        public IList<PartsOfActivityDto> GetAll()
        {
            IList<PartsOfActivityDto> list = new List<PartsOfActivityDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new PartsOfActivityDto(VARIABLE));
            }
            return list;
        }

        public PartsOfActivityDto Get(int ID)
        {
            return new PartsOfActivityDto(base.Get(ID));
        }

        public void Edit(PartsOfActivityDto dto)
        {
            PartsOfActivity entity = dto.ToPartsOfActivity();
            base.Edit(entity);
        }
    }
}