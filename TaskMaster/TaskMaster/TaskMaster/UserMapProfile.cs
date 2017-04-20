using AutoMapper;
using TaskMaster.Models;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Favorites, FavoritesDto>();
            CreateMap<Activities, ActivitiesDto>();
            CreateMap<PartsOfActivity, PartsOfActivityDto>();
            CreateMap<Tasks, TasksDto>();
            CreateMap<UserDto, User>();
            CreateMap<FavoritesDto, Favorites>();
            CreateMap<ActivitiesDto, Activities>();
            CreateMap<PartsOfActivityDto, PartsOfActivity>();
            CreateMap<TasksDto, Tasks>();
        }
    }
}
