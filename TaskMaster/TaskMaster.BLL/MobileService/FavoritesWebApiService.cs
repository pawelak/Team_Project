using System;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Models;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
 
    public class FavoritesWebApiService
    {
        private readonly FavoritesRepositories _favoritesRepositories = new FavoritesRepositories();
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TaskRepositories _taskRepositories = new TaskRepositories();

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
            var tmpTask = _taskRepositories.Get(favoritesMobileDto.Task.Name) ?? new TaskDto()
            {
                Description = "",
                Name = favoritesMobileDto.Task.Name
            };
            try
            {
                var fav = new FavoritesDto
                {
                    User = _userRepositories.Get(favoritesMobileDto.UserEmail),
                    Task = tmpTask
                };
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool deleteFromFav(FavoritesMobileDto favoritesMobileDto)
        {
            var del =
                _favoritesRepositories.Get(favoritesMobileDto.UserEmail)
                    .First(t => t.Task.Name.Equals(favoritesMobileDto.Task.Name));
            try
            {
                _favoritesRepositories.Delete(del);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }




    }
}