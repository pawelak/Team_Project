using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.ViewModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

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
            UserDto obj = TestService.GetUserByIde(1);
            Mapper.Initialize(ctg =>
                {
                    ctg.AddProfile(new MapperProfileBLL());
                    //ctg.a
                }
            );
            return Mapper.Map<UserViewModels>(obj);
        }

    }
}
