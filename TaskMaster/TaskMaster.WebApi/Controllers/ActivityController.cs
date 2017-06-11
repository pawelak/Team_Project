using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;


namespace TaskMaster.WebApi.Controllers
{

  
    public class ActivityController : ApiController
    {
        private readonly ActivityWebApiService _activityWebApiService = new ActivityWebApiService();
        private readonly VeryficationService _veryficationService = new VeryficationService();

        // GET: api/Activity/email
        public JsonResult <List<ActivityMobileDto>> Get(string email, string token)
        {
            if (_veryficationService.Authorization(email,token))
            {
                var result = _activityWebApiService.GetActivityFromLastWeek(email);
                return Json(result);
            }
            return null;
        }



        // PUT: api/Activity
        public HttpResponseMessage Put([FromBody]ActivityMobileDto activityMobileDto)
        {
            if (_veryficationService.Authorization(activityMobileDto.UserEmail, activityMobileDto.Token))
            {
                if (_activityWebApiService.AddActivity(activityMobileDto))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

    }
}
