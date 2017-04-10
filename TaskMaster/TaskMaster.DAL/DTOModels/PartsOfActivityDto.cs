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
        public PartsOfActivityDto(PartsOfActivity X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PartsOfActivity, PartsOfActivityDto>());
            Mapper.Map<PartsOfActivity, PartsOfActivityDto>(X, this);
        }
        public PartsOfActivity ToPartsOfActivity()
        {
            PartsOfActivity X = new PartsOfActivity();
            Mapper.Initialize(cfg => cfg.CreateMap<PartsOfActivityDto, PartsOfActivity>());
            Mapper.Map<PartsOfActivityDto, PartsOfActivity>(this, X);
            return X;
        }

        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }

        public ActivityDto Activity { get; set; }
    }
}