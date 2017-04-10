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
        public GroupDto(Group X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Group, GroupDto>());
            Mapper.Map<Group, GroupDto>(X, this);
        }
        public Group ToGroup()
        {
            Group X = new Group();
            Mapper.Initialize(cfg => cfg.CreateMap<GroupDto, Group>());
            Mapper.Map<GroupDto, Group>(this, X);
            return X;
        }

        public string NameGroup { get; set; }
 
        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<UserGroupDto> UserGroup { get; set; }
    }
}