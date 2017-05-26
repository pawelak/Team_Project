using TaskMaster.BLL.MobileServices;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
 
    public class FavoritesWebApiService
    {
        private readonly FavoritesRepositories _favoritesRepositories = new FavoritesRepositories();
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();

       


    }
}