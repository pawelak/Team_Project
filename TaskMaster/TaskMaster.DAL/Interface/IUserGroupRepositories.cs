using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IUserGroupRepositories
    {
        void Add(UserGroupDto dto);
        void Attach(UserGroupDto dto);
        void Delete(UserGroupDto dto);
        IList<UserGroupDto> GetAll();
        UserGroupDto Get(int id);
        IList<UserGroupDto> Get(string email);
        void Edit(UserGroupDto dto);
    }
}