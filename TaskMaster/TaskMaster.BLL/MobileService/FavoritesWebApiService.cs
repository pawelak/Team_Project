using TaskMaster.BLL.MobileModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
 
    public class FavoritesWebApiService
    {
        private readonly FavoritesRepositories _favoritesRepositories = new FavoritesRepositories();
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();

        public FavoritesWebApi GetAllFavoritesForEmail(string email)
        {
            if (!_userWebApiService.IsEmailInDatabase(email)) return null;
            var favoritesWebApi = new FavoritesWebApi {UserEmail = email};
            var listOfAllFavorites = _favoritesRepositories.GetAll();

            foreach (var fav in listOfAllFavorites)
            {
                if (fav.User.Email.Equals(email))
                {
                    var taskWebApi = new TaskWebApi
                    {
                        Name = fav.Task.Name,
                        //Description = fav.Task.Description
                    };
                    favoritesWebApi.Tasks.Add(taskWebApi);
                }
            }
            return favoritesWebApi;
        }


    }
}