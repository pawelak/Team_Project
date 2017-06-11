using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();
        private readonly PrintAllTestService _printAllTestService = new PrintAllTestService();



        // GET: api/User/email             
        public JsonResult<UserMobileDto> Get(string email)
        {
            return Json(_userWebApiService.GetUserByEmail(email));
        }


        // PUT: api/User  
        public JsonResult<string> Put([FromBody]UserMobileDto user, string jwtToken)
        {
            var tmp = _userWebApiService.AddNewUser(user, jwtToken);
            return Json(tmp);
        }
    }
}
