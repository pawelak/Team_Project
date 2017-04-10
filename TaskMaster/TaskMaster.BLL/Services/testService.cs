using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class TestService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();

        public UserDto GetUserByIde(int id)
        {
            return _userRepositories.GetUserById(id);
        }


        public void AddUserToDatabase(UserDto userDto)
        {
            _userRepositories.AddToDatabase(userDto);
        }


        public List<UserDto> GetAllInList()
        {
            return _userRepositories.GetAllToList();
        }

    }
}
