using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.WebApi.Controllers
{

  
    public class ActivityController : ApiController
    {
        private readonly ActivityWebApiService _activityWebApiService = new ActivityWebApiService();

        //// GET: api/Activity
        //public string Get()
        //{
        //    //var returnText = JsonConvert.SerializeObject(_activityWebApiService);
        //    return JsonConvert.SerializeObject(_activityWebApiService.test());
        //}

        // GET: api/Activity/email
        public string Get(string email)
        {
            return JsonConvert.SerializeObject(_activityWebApiService.GetActivityFromLastWeek(email));
        }

        // POST: api/Activity
        
        public HttpResponseMessage Post([FromBody]string request)
        {
            // var requestDto = JsonConvert.DeserializeObject(request);
            // return new HttpResponseMessage(HttpStatusCode.OK);
            return null;
        }

        // PUT: api/Activity/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Activity/5
        public void Delete(int id)
        {
        }
    }
}
