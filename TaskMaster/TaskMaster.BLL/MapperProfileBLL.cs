using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskMaster.BLL.ViewModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.BLL
{
    public class MapperProfileBLL : Profile 
    {
        public MapperProfileBLL()
        {
            this.CreateMap<UserDto, UserViewModels>();
        }
    }
}
