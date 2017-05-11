
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL
{
    public class MapperProfil : Profile
    {
        public MapperProfil()
        {
            this.CreateMap<ActivityDto, Activity>();
            this.CreateMap<FavoritesDto, Favorites>();
            this.CreateMap<GroupDto, Group>();
            this.CreateMap<PartsOfActivityDto, PartsOfActivity>();
            this.CreateMap<TaskDto, Task>();
            this.CreateMap<TokensDto, Tokens>();
            this.CreateMap<UserGroupDto, UserGroup>();
            this.CreateMap<UserDto, User>().ReverseMap();
        }
    }
}