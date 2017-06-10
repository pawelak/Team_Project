using System;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileService
{
 
    public class FavoritesWebApiService
    {
        private readonly FavoritesRepositories _favoritesRepositories = new FavoritesRepositories();
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

            var tmpTask = _taskRepositories.Get(favoritesMobileDto.Task.Name);
            if (tmpTask == null)
            {
                tmpTask = new TaskDto()
                {
                    Description = "",
                    Name = favoritesMobileDto.Task.Name,
                    Activities = new List<ActivityDto>(),
                    Favorites = new List<FavoritesDto>(),
                    Type = ""

                };
                _taskRepositories.Add(tmpTask);
            }
            var fav = new FavoritesDto()
            {
                Task = _taskRepositories.Get(tmpTask.Name),
                User = _userRepositories.Get(favoritesMobileDto.UserEmail)
            };
            try
            {
                _favoritesRepositories.Add(fav);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        public bool DeleteFromFavorites(FavoritesMobileDto favoritesMobileDto)
        {
            var del =
                _favoritesRepositories.Get(favoritesMobileDto.UserEmail)
                    .First(t => t.Task.Name.Equals(favoritesMobileDto.Task.Name));
            try
            {
                _favoritesRepositories.Delete(del);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }




    }
}