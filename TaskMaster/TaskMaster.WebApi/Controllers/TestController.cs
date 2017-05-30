using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.Services;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;
using TaskMaster.WebApi.Models;

namespace TaskMaster.WebApi.Controllers
{
    
    public class TestController : ApiController
    {
        readonly UserService _testService = new UserService();
        readonly UserWebApiService _userWebApiService =new UserWebApiService();
        readonly ActivityWebApiService _activityWebApiService = new ActivityWebApiService();

        // GET: api/Test
        public List<ActivityDto> Get()
        {
            return null;
        }

        // GET: api/Test/5
        public JsonResult<UserDto> Get(int id)
        {
            return null;
        }

        // POST: api/Test
        public void Post([FromBody]string userWebApi)
        {
            
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
