using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class PartsOfActivity
    {
        public PartsOfActivity(Models.PartsOfActivity X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.PartsOfActivity, PartsOfActivity>());
            Mapper.Map<Models.PartsOfActivity, PartsOfActivity>(X, this);
        }
        
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public DateTime Duration { get; set; }

        public Activity Activity { get; set; }

    }
}