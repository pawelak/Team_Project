using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IActivityRepositories
    {
        int Add(ActivityDto dto);
        void Delete(ActivityDto dto);
        IList<ActivityDto> GetAll();
        ActivityDto Get(int id);
        IList<ActivityDto> Get(string email);
        void Edit(ActivityDto dto);
    }
}