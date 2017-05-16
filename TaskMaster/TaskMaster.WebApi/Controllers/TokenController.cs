using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class TokenController : ApiController
    {
        private readonly UserWebApiService _userWebApi = new UserWebApiService();

        // POST: api/Token                                     edutuje lub nadpisze stary o zadanej platformie
        public void Post([FromBody]UserMobileDto userMobileDto)
        {
            _userWebApi.UpdateToken(userMobileDto);
        }

    }
}
