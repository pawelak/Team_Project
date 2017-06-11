using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private readonly TaskWebApiService _taskWebApiService = new TaskWebApiService();

        // GET: api/Task/name
        public JsonResult<TasksMobileDto> Get(string name)
        {

            return Json(_taskWebApiService.GetTask(name));
        }


        // PUT: api/Task/5
        public HttpResponseMessage Put([FromBody]TasksMobileDto tasksMobileDto)
        {
            if (_taskWebApiService.AddTask(tasksMobileDto))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


    }
}
