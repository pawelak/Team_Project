using System.Collections.Generic;
using System.Linq;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
 
    public class FavoritesWebApiService
    {
        private readonly FavoritesRepositories _favoritesRepositories = new FavoritesRepositories();
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();
        private readonly UserRepositories _userRepositories = new UserRepositories();


        public List<FavoritesMobileDto> GetAllFavorites(string email)
        {
            var user = _userRepositories.Get(email);

            var favorites = user.Favorites;
            var returnedFav = new List<FavoritesMobileDto>();

            foreach (var fav in favorites)
            {
                var tmpFav = new FavoritesMobileDto()
                {
                    UserEmail = user.Email,
                    Token = null,
                    EditState = EditState.None,
                    Task = new TasksMobileDto()
                    {
                        Name = fav.Task.Name,
                        Type = fav.Task.Type
                    }
                };
                returnedFav.Add(tmpFav);
            }

            return returnedFav;
        }

        public bool AddFavorites(FavoritesMobileDto favoritesMobileDto)
        {



            return false;
        }




    }
}