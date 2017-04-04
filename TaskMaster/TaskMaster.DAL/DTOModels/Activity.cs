using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class Activity
    {
        public Activity(Models.Activity X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Activity,Activity>());
            Mapper.Map<Models.Activity,Activity>(X, this);
        }
        
        public State State { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
        public Task Task { get; set; }

        public ICollection<PartsOfActivity> PartsOfActivity { get; set; }
        
    }

    public enum State
    {
        Planed,
        Started,
        Paused,
        Ended
    }
}