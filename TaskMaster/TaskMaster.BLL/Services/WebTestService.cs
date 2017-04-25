using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.ViewModels;

namespace TaskMaster.BLL.Services
{
    public class WebTestService
    {
        public TestService TestService = new TestService();

        public IList<UserViewModels> GetAllInListViewModels()
        {
            return TestService.GetAllInList().Select(variable => Mapper.Map<UserViewModels>(variable)).ToList();
        }

        public UserViewModels GetUser()
        {
            return Mapper.Map<UserViewModels>(TestService.GetUserByIde(1));
        }

    }
}
