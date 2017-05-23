using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.WebModels;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.BLL.WebServices
{
    public class WebUserService
    {
        private readonly UserService _userService = new UserService();

        public List<UserViewModels> GetUserList()
        {
            return _userService.GetUserList().Select(Mapper.Map<UserViewModels>).ToList();
        }

        public UserViewModels GetUser()
        {
           const int id = 1;
           return Mapper.Map<UserViewModels>(_userService.GetUser(id));
        }

        public void SaveUser(string email, string login, string password)
        {
            _userService.SaveUser(email,login,password);
        }
    }
}
