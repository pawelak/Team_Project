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
        public FavoritesDto(Models.Favorites X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Favorites, FavoritesDto>());
            Mapper.Map<Models.Favorites, FavoritesDto>(X, this);
        }
        

        public UserDto User { get; set; }
        public TaskDto Task { get; set; }

    }
}