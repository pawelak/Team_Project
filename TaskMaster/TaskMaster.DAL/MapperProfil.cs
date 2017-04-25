
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class MapperProfil : Profile
    {
        public MapperProfil()
        {
            this.CreateMap<ActivityDto, Activity>().ReverseMap();
            this.CreateMap<FavoritesDto, Favorites>().ReverseMap();
            this.CreateMap<GroupDto, Group>().ReverseMap();
            this.CreateMap<PartsOfActivityDto, PartsOfActivity>().ReverseMap();
            this.CreateMap<TaskDto, Task>().ReverseMap();
            this.CreateMap<TokensDto, Tokens>().ReverseMap();
            this.CreateMap<UserGroupDto, UserGroup>().ReverseMap();
            this.CreateMap<UserDto, User>().ReverseMap();
        }
    }
}