using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class User
    {
        public User(Models.User X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.User, User>());
            Mapper.Map<Models.User, User>(X, this);
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<Activity> Activity { get; set; }
        public ICollection<Tokens> Tokens { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }
        public ICollection<Favorites> Favorites { get; set; }

    }
}