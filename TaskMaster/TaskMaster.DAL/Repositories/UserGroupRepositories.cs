using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserGroupRepositories : RepoBase<UserGroup>
    {
        public UserGroupRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.UserGroup, UserGroupDto>());
          //  Mapper.Map<Models.UserGroup, UserGroupDto>(X, this);
        }

    }
}