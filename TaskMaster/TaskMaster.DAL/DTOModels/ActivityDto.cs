﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class ActivityDto
    {
       public State State { get; set; }
        public string Comment { get; set; }

        public UserDto User { get; set; }
        public GroupDto Group { get; set; }
        public TaskDto Task { get; set; }

        public ICollection<PartsOfActivityDto> PartsOfActivity { get; set; }
        
    }

    public enum State
    {
        Planed,
        Started,
        Paused,
        Ended
    }
}