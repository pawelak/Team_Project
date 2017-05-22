using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.ViewModels;

namespace TaskMaster.BLL.WebServices
{
    public class WebTestService
    {
        private readonly TestService _testService = new TestService();
        private readonly MainService _mainService = new MainService();

        public List<UserViewModels> GetAllInListViewModels()
        {
            return _testService.GetAllInList().Select(Mapper.Map<UserViewModels>).ToList();
        }

        public UserViewModels GetUser()
        {
            const int id = 1;
            return Mapper.Map<UserViewModels>(_testService.GetUserByIde(id));
        } 

        public void Pomnoz(string email, string login = "A", string password = "B")
        {
            _mainService.Stworz(email, login, password);
        }

    }
}
