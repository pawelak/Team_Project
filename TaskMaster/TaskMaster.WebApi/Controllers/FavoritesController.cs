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
    public class FavoritesController : ApiController
    {
        private readonly FavoritesWebApiService _favoritesWebApiService = new FavoritesWebApiService();
        private readonly VeryficationService _veryficationService = new VeryficationService();

        // GET: api/Favorites/email
        public JsonResult <List<FavoritesMobileDto>>  Get(string email, string token)
        {
            if (_veryficationService.Authorization(email, token))
            {
                var result = _favoritesWebApiService.GetAllFavorites(email);
                return Json(result);
            }
            return null;
        }


        // PUT: api/Favorites
        public HttpResponseMessage Put([FromBody]FavoritesMobileDto favoritesMobileDto)
        {
            if (_veryficationService.Authorization(favoritesMobileDto.UserEmail, favoritesMobileDto.Token))
            {
                if (_favoritesWebApiService.AddFavorites(favoritesMobileDto))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }


        // DELETE: api/Favorites
        public HttpResponseMessage Delete([FromBody]FavoritesMobileDto favoritesMobileDto)
        {
            if (_veryficationService.Authorization(favoritesMobileDto.UserEmail, favoritesMobileDto.Token))
            {
                if (_favoritesWebApiService.DeleteFromFavorites(favoritesMobileDto))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}
