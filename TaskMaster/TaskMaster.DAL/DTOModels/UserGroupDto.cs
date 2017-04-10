using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class UserGroupDto
    {
        public UserDto User { get; set; }
        public GroupDto Group { get; set; }

    }
}