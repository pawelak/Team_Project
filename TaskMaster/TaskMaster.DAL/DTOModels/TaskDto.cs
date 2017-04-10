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
        public TaskDto(Task X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Task, TaskDto>());
            Mapper.Map<Task, TaskDto>(X, this);
        }

        public Task ToTask()
        {
            Task X = new Task();
            Mapper.Initialize(cfg => cfg.CreateMap<TaskDto, Task>());
            Mapper.Map<TaskDto, Task>(this, X);
            return X;
        }

        public string Name { get; set; }
        public string Descryption { get; set; }

        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<FavoritesDto> Favorites { get; set; }
    }
}