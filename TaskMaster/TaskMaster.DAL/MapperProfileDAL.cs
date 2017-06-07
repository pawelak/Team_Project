using System;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class MapperProfileDal : Profile
    {
        private const int Dep = 1;

        public MapperProfileDal()
        {
            CreateMap<ActivityDto, Activity>()
                .MaxDepth(Dep);
            CreateMap<Activity, ActivityDto>()
                .MaxDepth(Dep);
            CreateMap<FavoritesDto, Favorites>()
                .MaxDepth(Dep);
            CreateMap<Favorites, FavoritesDto>()
                .MaxDepth(Dep);
            CreateMap<GroupDto, Group>()
                .MaxDepth(Dep);
            CreateMap<Group, GroupDto>()
                .MaxDepth(Dep);
            CreateMap<TaskDto, Task>()
                .MaxDepth(Dep);
            CreateMap<Task, TaskDto>()
                .MaxDepth(Dep);
            CreateMap<TokensDto, Tokens>()
                .MaxDepth(Dep);
            CreateMap<Tokens, TokensDto>()
                .MaxDepth(Dep);
            CreateMap<UserGroupDto, UserGroup>()
                .MaxDepth(Dep);
            CreateMap<UserGroup, UserGroupDto>()
                .MaxDepth(Dep);
            CreateMap<UserDto, User>()
                .MaxDepth(Dep);
            CreateMap<User, UserDto>()
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