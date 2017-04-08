using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class Favorites
    {
        public Favorites(Models.Favorites X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Favorites, Favorites>());
            Mapper.Map<Models.Favorites, Favorites>(X, this);
        }
        

        public User User { get; set; }
        public Task Task { get; set; }

    }
}