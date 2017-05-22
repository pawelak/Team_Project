using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.ViewModels;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.BLL.WebServices
{
    public class WebTestService
    {
        private readonly TestService _testService = new TestService();

        public List<UserViewModels> GetAllInListViewModels()
        {
            return _testService.GetAllInList().Select(Mapper.Map<UserViewModels>).ToList();
        }

        public UserViewModels GetUser()
        {
           const int id = 1;
           // var obj = _testService.GetUserByIde(id);
           
           return Mapper.Map<UserViewModels>(new UserDto());
        }

    }
}
