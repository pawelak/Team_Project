using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;
    

namespace TaskMaster.DAL.Repositories
{
    public class ActivityRepositories : RepoBase<Activity>
    {
        public ActivityRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Activity, ActivityDto>());
            //Mapper.Map<Models.Activity, ActivityDto>(X, this);
        }


    }
}