using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class Task
    {
        public Task(Models.Task X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Task, Task>());
            Mapper.Map<Models.Task, Task>(X, this);
        }
        
        public string Name { get; set; }
        public string Descryption { get; set; }

        public ICollection<Activity> Activity { get; set; }
        public ICollection<Favorites> Favorites { get; set; }

    }
}