using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class FavoritesRepositories : RepoBase<Favorites>, IFavoritesRepositories
    {
        public FavoritesRepositories()
        {

        }

        public void Add(FavoritesDto dto)
        {
            Favorites entity = dto.ToFavorites();
            base.Add(entity);
        }

        public void Delete(FavoritesDto dto)
        {
            Favorites entity = dto.ToFavorites();
            base.Delete(entity);
        }

        public IList<FavoritesDto> GetAll()
        {
            IList<FavoritesDto> list = new List<FavoritesDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new FavoritesDto(VARIABLE));
            }
            return list;
        }

        public FavoritesDto Get(int ID)
        {
            return new FavoritesDto(base.Get(ID));
        }

        public void Edit(FavoritesDto dto)
        {
            Favorites entity = dto.ToFavorites();
            base.Edit(entity);
        }

    }
}