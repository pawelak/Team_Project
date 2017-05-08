using AutoMapper;
using TaskMaster.Models;
using TaskMaster.ModelsDto;

namespace TaskMaster
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Favorites, FavoritesDto>().ReverseMap();
            CreateMap<Activities, ActivitiesDto>().ReverseMap();
            CreateMap<PartsOfActivity, PartsOfActivityDto>().ReverseMap();
            CreateMap<Tasks, TasksDto>().ReverseMap();
        }
    }
}
