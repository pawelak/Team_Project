using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class TaskDto
    {
        public TaskDto(Models.Task X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Task, TaskDto>());
            Mapper.Map<Models.Task, TaskDto>(X, this);
        }
        
        public string Name { get; set; }
        public string Descryption { get; set; }

        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<FavoritesDto> Favorites { get; set; }

    }
}