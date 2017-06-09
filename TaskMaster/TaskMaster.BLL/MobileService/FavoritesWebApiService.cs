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
        // TODO interfejsy!
        private readonly FavoritesRepositories _favoritesRepositories = new FavoritesRepositories();
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();
        private readonly UserRepositories _userRepositories = new UserRepositories();


        public List<FavoritesMobileDto> GetAllFavorites(string email)
        {
            // TODO to powinno byc pobranie z tabeli favorites where userId == id, bo po co pobierany jest tu User?

            var user = _userRepositories.Get(email);

            var favorites = user.Favorites;
            var returnedFav = new List<FavoritesMobileDto>();

            // TODO całego foreach'a mozna zastąpic mapowanie, automapper radzi sobie z mapowaniem kolekcji na kolekcję
            foreach (var fav in favorites)
            {
                // TODO automapper
                var tmpFav = new FavoritesMobileDto()
                {
                    UserEmail = user.Email,
                    Token = null,
                    EditState = EditState.None,
                    
                    // TODO automapper
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




    }
}