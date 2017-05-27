
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
            CreateMap<PartsOfActivityDto, PartsOfActivity>()
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
        }
    }
}