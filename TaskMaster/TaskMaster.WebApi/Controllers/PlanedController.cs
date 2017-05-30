using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.WebApi.Controllers
{
    public class PlanedController : ApiController
    {
        private readonly PlannedService _activityPlanned = new PlannedService();

        // GET: api/Planed
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Planed/email
        [ResponseType(typeof(string))]
        public HttpResponseMessage Get(HttpRequestMessage request, string email)
        {
            return request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_activityPlanned.GetPlanned(email)));
        }

        // POST: api/Planed
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Planed/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Planed/5
        public void Delete(int id)
        {
        }
    }
}
