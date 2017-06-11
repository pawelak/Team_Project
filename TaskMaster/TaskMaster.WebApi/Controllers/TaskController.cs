using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private readonly TaskWebApiService _taskWebApiService = new TaskWebApiService();
        private readonly VeryficationService _veryficationService = new VeryficationService();




        // GET: api/Task/name
        public JsonResult<TasksMobileDto> Get(string name, string email, string token)
        {
            if (_veryficationService.Authorization(email, token))
            {
                return Json(_taskWebApiService.GetTask(name));
            }
            return null;
        }


        // PUT: api/Task/5
        public HttpResponseMessage Put([FromBody]TasksMobileDto tasksMobileDto)
        {
            if (_veryficationService.Authorization(tasksMobileDto.Email, tasksMobileDto.Token))
            {
                if (_taskWebApiService.AddTask(tasksMobileDto))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }


    }
}
