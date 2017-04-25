using System.Collections.Generic;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class FavoritesRepositories : RepoBase<Favorites>, IFavoritesRepositories
    {
        public FavoritesRepositories()
        {
            Mapper.Initialize(ctg => ctg.AddProfile(new MapperProfil()));
        }

        public void Add(FavoritesDto dto)
        {
            base.Add(Mapper.Map<Favorites>(dto));
        }

        public void Delete(FavoritesDto dto)
        {
            base.Delete(Mapper.Map<Favorites>(dto));
        }

        public IList<FavoritesDto> GetAll()
        {
            IList<FavoritesDto> list = new List<FavoritesDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<FavoritesDto>(VARIABLE));
            }
            return list;
        }

        public new FavoritesDto Get(int ID)
        {
            return Mapper.Map<FavoritesDto>(base.Get(ID));
        }

        public void Edit(FavoritesDto dto)
        {
            base.Edit(Mapper.Map<Favorites>(dto));
        }

    }
}