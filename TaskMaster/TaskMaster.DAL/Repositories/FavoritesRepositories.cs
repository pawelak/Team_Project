using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class FavoritesRepositories : RepoBase<Favorites>, IFavoritesRepositories
    {
        public void Add(FavoritesDto dto)
        {
            base.Add(Mapper.Map<Favorites>(dto));
        }

        public void Delete(FavoritesDto dto)
        {
            base.Delete(Mapper.Map<Favorites>(dto));
        }

        public new IList<FavoritesDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<FavoritesDto>).ToList();
        }

        public new FavoritesDto Get(int id)
        {
            return Mapper.Map<FavoritesDto>(base.Get(id));
        }

        public IList<FavoritesDto> Get(string email)
        {
            var list = GetAll();
            return list.Where(l => l.User.Email.Equals(email)).ToList();
        }

        public void Edit(FavoritesDto dto)
        {
            base.Edit(Mapper.Map<Favorites>(dto),"FavoritesId");
        }

    }
}