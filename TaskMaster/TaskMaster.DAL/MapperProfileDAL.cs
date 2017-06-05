using System;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class MapperProfileDal : Profile
    {
        private const int Dep = 2;

        public MapperProfileDal()
        {
            CreateMap<ActivityDto, Activity>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<FavoritesDto, Favorites>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<GroupDto, Group>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<TaskDto, Task>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<TokensDto, Tokens>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<UserGroupDto, UserGroup>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<UserDto, User>()
                .ReverseMap()
                .MaxDepth(Dep);
            CreateMap<PartsOfActivity, PartsOfActivityDto>()
                .MaxDepth(Dep)
                .ForMember(dest => dest.Duration,
                    opt => opt.MapFrom
                    (src =>src.Duration.Subtract(new DateTime(2000, 1, 1, 0, 0, 0))));
            CreateMap<PartsOfActivityDto, PartsOfActivity>()
                .MaxDepth(Dep)
                .ForMember(dest => dest.Duration,
                    opt => opt.MapFrom
                    (src => new DateTime(2000, 1, 1, 0, 0, 0).Add(src.Duration)));


        }
    }
}