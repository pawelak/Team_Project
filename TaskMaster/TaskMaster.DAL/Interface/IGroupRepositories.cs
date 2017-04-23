using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IGroupRepositories
    {
        void Add(GroupDto dto);
        void Delete(GroupDto dto);
        IList<GroupDto> GetAll();
        GroupDto Get(int id);
        void Edit(GroupDto dto);
    }
}