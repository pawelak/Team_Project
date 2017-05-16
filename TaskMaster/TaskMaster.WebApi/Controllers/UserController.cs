using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();
        private readonly PrintAllTestService _printAllTestService = new PrintAllTestService();

        // GET: api/User
        public List<UserMobileDto> Get()
        {
            return _printAllTestService.PrintAllUserWebApi();
        }

        // GET: api/User/tekst
        public UserMobileDto Get(string email)
        {
            return _userWebApiService.GetUserByEmail(email);
        }

        // POST: api/User
        public void Post([FromBody]UserMobileDto userMobileDto)
        {
            if (_userWebApiService.EditUser(userMobileDto))
            {
                Console.WriteLine("error, user can't be edit");
            }

        }

        // PUT: api/User
        public void Put([FromBody]UserMobileDto userMobileDto)
        {
            if (_userWebApiService.AddNewUser(userMobileDto))
            {
                Console.WriteLine("error, user can't be added");
            }
        }

        // DELETE: api/User
        public void Delete([FromBody]UserMobileDto userMobileDto)
        {
            if (_userWebApiService.DeleteUserByEmail(userMobileDto))
            {
                Console.WriteLine("user not deleted");
            }
        }
    }
}
