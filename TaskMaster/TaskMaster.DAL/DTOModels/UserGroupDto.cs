using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class UserGroupDto
    {
        public UserGroupDto(Models.UserGroup X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.UserGroup, UserGroupDto>());
            Mapper.Map<Models.UserGroup, UserGroupDto>(X, this);
        }

        public UserDto User { get; set; }
        public GroupDto Group { get; set; }

    }
}