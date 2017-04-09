using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IPartsOfActivityRepositories
    {
        void Add(PartsOfActivityDto dto);
        void Delete(PartsOfActivityDto dto);
        IList<PartsOfActivityDto> GetAll();
        PartsOfActivityDto Get(int ID);
        void Edit(PartsOfActivityDto dto);
    }
}