using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TaskRepositories : RepoBase<Task>
    {
        public TaskRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Task, TaskDto>());
           // Mapper.Map<Models.Task, TaskDto>(X, this);
        }

    }
}