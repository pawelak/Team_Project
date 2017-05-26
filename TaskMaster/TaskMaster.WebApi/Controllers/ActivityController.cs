using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;

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
            
            //var returnText = JsonConvert.SerializeObject(_activityWebApiService.test2());
            var returnText = JsonConvert.SerializeObject(_activityWebApiService.GetActivityFromLastWeek(email));
            return returnText;
        }

        // POST: api/Activity
        public void Post([FromBody]string value)
        {
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
