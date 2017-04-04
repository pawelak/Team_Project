using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class Group
    {
        public Group(Models.Group X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Group, Group>());
            Mapper.Map<Models.Group, Group>(X, this);
        }
        
        public string NameGroup { get; set; }
 
        public ICollection<Activity> Activity { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }

    }
}