using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;
using TaskMaster.WebApi.Models;

namespace TaskMaster.BLL.WebApiServices
{
 
    public class FavoritesWebApiService
    {
        private readonly FavoritesRepositories favoritesRepositories = new FavoritesRepositories();
        private readonly UserWebApiService userWebApiService = new UserWebApiService();

        public FavoritesWebApi GetAllFavoritesForEmail(string email)
        {
            if (userWebApiService.IsEmailInDatabase(email))
            {
                var favoritesWebApi = new FavoritesWebApi {UserEmail = email};
                var listOfAllFavorites = favoritesRepositories.GetAll();

                foreach (var fav in listOfAllFavorites)
                {
                    if (fav.User.Email.Equals(email))
                    {
                        var taskWebApi = new TaskWebApi
                        {
                            Name = fav.Task.Name,
                            Description = fav.Task.Description
                        };
                        favoritesWebApi.Tasks.Add(taskWebApi);
                    }
                }
                return favoritesWebApi;
            }
            return null;
        }

       


    }
}