using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class Tokens
    {
        public Tokens(Models.Tokens X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Tokens, Tokens>());
            Mapper.Map<Models.Tokens, Tokens>(X, this);
        }

        public string Token { get; set; }
        public BrowserType BrowserType { get; set; }
        public PlatformType PlatformType { get; set; }

        public User User { get; set; }
    }

        public enum BrowserType
        {
            Firefox,
            Chrome,
            Opera,
            Safari
        }

        public enum PlatformType
        {
            Android,
            WindowsPhone,
            Ios
        }
   
}