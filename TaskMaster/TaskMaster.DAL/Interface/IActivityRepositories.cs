using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IActivityRepositories
    {
        void Add(ActivityDto dto);
        void Delete(ActivityDto dto);
        IList<ActivityDto> GetAll();
        ActivityDto Get(int id);
        void Edit(ActivityDto dto);
    }
}