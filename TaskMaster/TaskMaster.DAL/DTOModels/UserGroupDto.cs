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
        public UserGroupDto(UserGroup X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserGroup, UserGroupDto>());
            Mapper.Map<UserGroup, UserGroupDto>(X, this);
        }

        public UserGroup ToUserGroup()
        {
            UserGroup X = new UserGroup();
            Mapper.Initialize(cfg => cfg.CreateMap<UserGroupDto, UserGroup>());
            Mapper.Map<UserGroupDto, UserGroup>(this, X);
            return X;
        }

        public UserDto User { get; set; }
        public GroupDto Group { get; set; }
    }
}