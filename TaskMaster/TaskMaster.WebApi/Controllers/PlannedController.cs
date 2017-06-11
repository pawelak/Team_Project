using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class PlannedController : ApiController
    {
        private readonly PlannedWebApiService _plannedWebApiService = new PlannedWebApiService();
        private readonly VeryficationService _veryficationService = new VeryficationService();


        // GET: api/Planned/email
        public JsonResult<List<PlannedMobileDto>> Get(string email, string token)
        {
            if (_veryficationService.Authorization(email, token))
            {
                var result = _plannedWebApiService.GetPlanned(email);
                return Json(result);
            }
            return null;
        }

       

        // PUT: api/Planned
        public HttpResponseMessage Put([FromBody]PlannedMobileDto activityMobileDto)
        {
            if (_veryficationService.Authorization(activityMobileDto.UserEmail, activityMobileDto.Token))
            {
                var x = activityMobileDto;
                if (_plannedWebApiService.AddPlanned(activityMobileDto))
                {

                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }


        // POST: api/Planned
        public HttpResponseMessage Post(PlannedMobileDto plannedMobileDto)
        {
            if (_veryficationService.Authorization(plannedMobileDto.UserEmail, plannedMobileDto.Token))
            {
                if (_plannedWebApiService.EndPlanned(plannedMobileDto))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }



        // DELETE: api/Planned
        public HttpResponseMessage Delete(string guid, string email, string token)
        {
            if (_veryficationService.Authorization(email, token))
            {
                if (_plannedWebApiService.Delete(guid,email))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }


    }
}
