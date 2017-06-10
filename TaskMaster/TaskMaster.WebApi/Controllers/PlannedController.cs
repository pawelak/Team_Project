using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class PlannedController : ApiController
    {
        private readonly PlannedWebApiService _plannedWebApiService = new PlannedWebApiService();


        // GET: api/Planned/email
        public JsonResult<List<PlannedMobileDto>> Get(string email)
        {
            var result = _plannedWebApiService.GetPlanned(email);
            return Json(result);
        }

       

        // PUT: api/Planned
        public HttpResponseMessage Put([FromBody]PlannedMobileDto activityMobileDto)
        {
            var x = activityMobileDto;
            if (_plannedWebApiService.AddPlanned(activityMobileDto))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // DELETE: api/PLanned
        public HttpResponseMessage Delete(PlannedMobileDto plannedMobileDto)
        {
            if (_plannedWebApiService.Delete(plannedMobileDto))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


    }
}
