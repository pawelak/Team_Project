using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class PartsOfActivityRepositories : RepoBase<PartsOfActivity>
    {
        public PartsOfActivityRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.PartsOfActivity, PartsOfActivityDto>());
            //Mapper.Map<Models.PartsOfActivity, PartsOfActivityDto>(X, this);
        }

    }
}