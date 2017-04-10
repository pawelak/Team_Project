using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class FavoritesDto
    {
        public FavoritesDto(Favorites X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Favorites, FavoritesDto>());
            Mapper.Map<Favorites, FavoritesDto>(X, this);
        }
        public Favorites ToFavorites()
        {
            Favorites X = new Favorites();
            Mapper.Initialize(cfg => cfg.CreateMap<FavoritesDto, Favorites>());
            Mapper.Map<FavoritesDto, Favorites>(this, X);
            return X;
        }

        public UserDto User { get; set; }
        public TaskDto Task { get; set; }
    }
}