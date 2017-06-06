using System;
using System.Collections.Generic;
using System.Linq;
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

        // POST: api/Planned
        public void Post([FromBody]string value)
        {
        }


    }
}
