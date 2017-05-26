using System.Collections.Generic;
using System.Web.Http;

using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.Services;
using TaskMaster.DAL.DTOModels;
using TaskMaster.WebApi.Models;

namespace TaskMaster.WebApi.Controllers
{
    
    public class TestController : ApiController
    {
        readonly UserService _testService = new UserService();
        readonly UserWebApiService _userWebApiService =new UserWebApiService();

        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public List<UserDto> Get(int id)
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
