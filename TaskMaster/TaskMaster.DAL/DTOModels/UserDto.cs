using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class UserDto
    {
        public UserDto(User X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDto>());
            Mapper.Map<User, UserDto>(X, this);
        }

        public User ToUser()
        {
            User X = new User();
            Mapper.Initialize(cfg => cfg.CreateMap<UserDto, User>());
            Mapper.Map<UserDto, User>(this, X);
            return X;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<TokensDto> Tokens { get; set; }
        public ICollection<UserGroupDto> UserGroup { get; set; }
        public ICollection<FavoritesDto> Favorites { get; set; }
    }
}