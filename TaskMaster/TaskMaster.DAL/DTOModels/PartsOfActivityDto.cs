using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class PartsOfActivityDto
    {
        public PartsOfActivityDto(Models.PartsOfActivity X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.PartsOfActivity, PartsOfActivityDto>());
            Mapper.Map<Models.PartsOfActivity, PartsOfActivityDto>(X, this);
        }
        
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }

        public ActivityDto Activity { get; set; }

    }
}