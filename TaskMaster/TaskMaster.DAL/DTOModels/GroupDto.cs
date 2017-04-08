using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class GroupDto
    {
        public GroupDto(Models.Group X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Group, GroupDto>());
            Mapper.Map<Models.Group, GroupDto>(X, this);
        }
        
        public string NameGroup { get; set; }
 
        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<UserGroupDto> UserGroup { get; set; }

    }
}