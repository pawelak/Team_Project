
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class MapperProfileDAL : Profile
    {
        public MapperProfileDAL()
        {
            CreateMap<ActivityDto, Activity>().ReverseMap();
            CreateMap<FavoritesDto, Favorites>();
            CreateMap<GroupDto, Group>().ReverseMap();
            CreateMap<PartsOfActivityDto, PartsOfActivity>().ReverseMap();
            CreateMap<TaskDto, Task>().ReverseMap();
            CreateMap<TokensDto, Tokens>().ReverseMap();
            CreateMap<UserGroupDto, UserGroup>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}