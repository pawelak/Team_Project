
using System;
using System.Web.UI;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class MapperProfileDAL : Profile
    {
        static  int dep = 500;
        public MapperProfileDAL()
        {
            CreateMap<ActivityDto, Activity>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<FavoritesDto, Favorites>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<GroupDto, Group>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<TaskDto, Task>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<TokensDto, Tokens>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<UserGroupDto, UserGroup>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<UserDto, User>()
                .ReverseMap()
                .MaxDepth(dep);
            CreateMap<PartsOfActivity, PartsOfActivityDto>()
                .MaxDepth(dep)
                .ForMember(dest => dest.Duration,
                    opt => opt.MapFrom
                    (src =>src.Duration.Subtract(new DateTime(2000, 1, 1, 0, 0, 0))));
            CreateMap<PartsOfActivityDto, PartsOfActivity>()
                .MaxDepth(dep)
                .ForMember(dest => dest.Duration,
                    opt => opt.MapFrom
                    (src => new DateTime(2000, 1, 1, 0, 0, 0).Add(src.Duration)));


        }
    }
}