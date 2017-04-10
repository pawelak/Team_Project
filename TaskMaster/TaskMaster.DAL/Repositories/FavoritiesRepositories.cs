using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class FavoritiesRepositories : RepoBase<Favorites>
    {
        public FavoritiesRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Favorites, FavoritesDto>());
            //Mapper.Map<Models.Favorites, FavoritesDto>(X, this);
        }

    }
}