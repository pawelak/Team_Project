using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.DAL.DTOModels;
using TaskMaster.BLL.Services;

namespace TaskMaster.WebApi.Controllers
{
    public class TestController : ApiController
    {
        private readonly TestService _testService = new TestService();

        //// GET: api/Test/getone/1
        //public UserDto Get(int id)
        //{
        //    UserDto user = new UserDto();
        //    user.Name = "dupek:";
        //    return _testService.GetUserByIde(id);
        //}

        //// GET: api/Test/getall
        //public List<UserDto> Get()
        //{
        //    return _testService.PrintAll();
        //}

        //GET: api/Test/1/2
        public UserDto Get(int id =1)
        {
            
            return _testService.GetUserByIde(id);
        }


        // GET: api/Test/cos
        public List<UserDto> Get()
        {
            return _testService.GetAllInList();
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
