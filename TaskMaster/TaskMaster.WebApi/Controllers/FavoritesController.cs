using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using TaskMaster.BLL.MobileService;

namespace TaskMaster.WebApi.Controllers
{
    public class FavoritesController : ApiController
    {
        private readonly FavoritesWebApiService _favoritesWebApiService = new FavoritesWebApiService();

        // GET: api/Favorites
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Favorites/email
        [ResponseType(typeof(string))]
        public HttpResponseMessage Get(HttpRequestMessage request ,string email)
        {
            // TODO wywołanie metody, w metodzie, w metodzie
            // TODO wystarczy return _favoritesWebApiService.GetAllFavorites(email))
            return request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_favoritesWebApiService.GetAllFavorites(email)));
        }

        // POST: api/Favorites
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Favorites/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Favorites/5
        public void Delete(int id)
        {
        }
    }
}
