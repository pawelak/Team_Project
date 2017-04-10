using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IUserGroupRepositories
    {
        void Add(UserGroupDto dto);
        void Delete(UserGroupDto dto);
        IList<UserGroupDto> GetAll();
        UserGroupDto Get(int ID);
        void Edit(UserGroupDto dto);
    }
}