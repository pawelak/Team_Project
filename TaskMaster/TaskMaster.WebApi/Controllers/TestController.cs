using System.Collections.Generic;
using System.Web.Http;
using TaskMaster.BLL.Services;
using TaskMaster.DAL.DTOModels;
//using TaskMaster.BLL.MobileServices;

namespace TaskMaster.WebApi.Controllers
{
    
    public class TestController : ApiController
    {
        readonly TestService _testService = new TestService();
        //readonly 

        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public UserDto Get(int id)
        {
            return _testService.GetUserByIde(1);
        }

        // POST: api/Test
        public void Post([FromBody]string value)
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
