using System.Collections.Generic;
using System.Linq;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class TestService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();

        public UserDto GetUserByIde(int id)
        {
            return _userRepositories.Get(id);
        }


        //public void AddUserToDatabase(UserDto userDto)
        //{
        //    _userRepositories.AddToDatabase(userDto);
        //}


        public List<UserDto> GetAllInList()
        {
            return _userRepositories.GetAll().ToList();
        }




    }
}
