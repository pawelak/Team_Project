using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;


namespace TaskMaster.WebApi.Controllers
{

  
    public class ActivityController : ApiController
    {
        private readonly ActivityWebApiService _activityWebApiService = new ActivityWebApiService();


        // GET: api/Activity/email
        public JsonResult <List<ActivityMobileDto>> Get(string email)
        {
            var result = _activityWebApiService.GetActivityFromLastWeek(email);
            return Json(result);
        }

        // POST: api/Activity
        
        public void Post([FromBody]ActivityMobileDto activityMobileDto)
        {
            
        }

        // PUT: api/Activity
        public HttpResponseMessage Put([FromBody]ActivityMobileDto activityMobileDto)
        {
            if (_activityWebApiService.AddActivity(activityMobileDto))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }



    }
}
