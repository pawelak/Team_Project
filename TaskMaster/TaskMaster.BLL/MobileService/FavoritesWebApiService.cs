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

        //machlojona baza
        private DataSimulation _database = new DataSimulation();


        public List<FavoritesMobileDto> GetAllFavorites(string email)
        {

            //------------machlojenia z bazą----------------
            var user = _database.userDtosList.First(a => a.Email.Equals(email));
            //var users = _userRepositories.Get(email);

            var favorites = user.Favorites;
            var returnedFav = new List<FavoritesMobileDto>();

            foreach (var fav in favorites)
            {
                var taskHelp = new TasksMobileDto()
                {
                    Name = fav.Task.Name,
                    Type = fav.Task.Type
                };
                var tmpFav = new FavoritesMobileDto()
                {
                    UserEmail = user.Email,
                    Token = null,
                    EditState = EditState.None,
                    Task = taskHelp
                };
                returnedFav.Add(tmpFav);
            }

            return returnedFav;
        }




    }
}