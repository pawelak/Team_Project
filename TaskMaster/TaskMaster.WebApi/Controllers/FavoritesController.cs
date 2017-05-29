using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;

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
        public JsonResult<List<FavoritesMobileDto>> Get(string email)
        {
            var result = _favoritesWebApiService.GetAllFavorites(email);
            return Json(result);
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
