using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class UserGroup
    {
        public UserGroup(Models.UserGroup X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.UserGroup, UserGroup>());
            Mapper.Map<Models.UserGroup, UserGroup>(X, this);
        }

        public User User { get; set; }
        public Group Group { get; set; }

    }
}