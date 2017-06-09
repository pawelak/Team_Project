using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskMaster.WebApi.Controllers
{
    public class VeryficationController : ApiController
    {
        // GET: api/Veryfication
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Veryfication/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Veryfication
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Veryfication/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Veryfication/5
        public void Delete(int id)
        {
        }
    }
}
