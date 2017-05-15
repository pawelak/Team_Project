using AutoMapper;
using TaskMaster.BLL.ViewModels;
using TaskMaster.DAL.DTOModels;


namespace TaskMaster.BLL
{
    public class MapperProfileBLL : Profile 
    {
        public MapperProfileBLL()
        {
            this.CreateMap<UserDto, UserViewModels>().ReverseMap();
        }
    }
}
