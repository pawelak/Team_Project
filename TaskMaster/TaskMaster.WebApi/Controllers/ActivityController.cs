using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
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
        public JsonResult <List<ActivityMobileDto>> Get(string email)
        {
            var result = _activityWebApiService.GetActivityFromLastWeek(email);
            return Json(result);
        }

        // POST: api/Activity
        
        public HttpResponseMessage Post([FromBody]ActivityMobileDto activityMobileDto)
        {
            var x = activityMobileDto;
            if (_activityWebApiService.AddActivity(activityMobileDto))
            {

                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
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
