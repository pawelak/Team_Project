using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaskMaster.BLL.MobileService;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class FavoritesController : ApiController
    {
        private readonly FavoritesWebApiService _favoritesWebApiService = new FavoritesWebApiService();


        // GET: api/Favorites/email
        public JsonResult <List<FavoritesMobileDto>>  Get(string email)
        {
            var result = _favoritesWebApiService.GetAllFavorites(email);
            return Json(result);
        }


        // PUT: api/Favorites
        public HttpResponseMessage Put([FromBody]FavoritesMobileDto favoritesMobileDto)
        {
            if (_favoritesWebApiService.AddFavorites(favoritesMobileDto))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // DELETE: api/Favorites
        public HttpResponseMessage Delete([FromBody]FavoritesMobileDto favoritesMobileDto)
        {
            if (_favoritesWebApiService.DeleteFromFavorites(favoritesMobileDto))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
