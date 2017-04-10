using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class GroupRepositories : RepoBase<Group>
    {
        public GroupRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Group, GroupDto>());
           // Mapper.Map<Models.Group, GroupDto>(X, this);
        }


    }
}