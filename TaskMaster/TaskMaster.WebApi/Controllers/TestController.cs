using System.Collections.Generic;
using System.Web.Http;
using TaskMaster.BLL.Services;
using TaskMaster.BLL.WebApiServices;
using TaskMaster.DAL.DTOModels;
using TaskMaster.WebApi.Models;

namespace TaskMaster.WebApi.Controllers
{
    
    public class TestController : ApiController
    {
        readonly TestService _testService = new TestService();
        readonly PrintAll _printAll = new PrintAll();
        readonly UserWebApiService _userWebApiService =new UserWebApiService();

        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public List<UserDto> Get(int id)
        {
            return _printAll.PrintAllUserDtos();
        }

        // POST: api/Test
        public void Post([FromBody]UserWebApi userWebApi)
        {
            _userWebApiService.AddNewUser(userWebApi);
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
