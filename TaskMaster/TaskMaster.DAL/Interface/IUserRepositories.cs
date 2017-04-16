using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IUserRepositories
    {
        void Add(UserDto dto);
        void Delete(UserDto dto);
        IList<UserDto> GetAll();
        UserDto Get(int id);
        void Edit(UserDto dto);
    }
}