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

        // GET: api/User               
        public List<UserMobileDto> Get()
        {
            return _printAllTestService.PrintAllUserWebApi();
        }

        // GET: api/User/email             
        public JsonResult<UserMobileDto> Get(string email)
        {
            return Json(_userWebApiService.GetUserByEmail(email));
        }

       

        // PUT: api/User                                    dodanie nowego użtkownika
        public HttpResponseMessage Put([FromBody]UserMobileDto user)
        {
            if (_userWebApiService.AddNewUser(user) == true)
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
          
        }

        // DELETE: api/User                                     usuwanie
        public HttpResponseMessage Delete([FromBody]UserMobileDto userMobileDto)
        {
            return !_userWebApiService.DeleteUserByEmail(userMobileDto)
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
