using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class TokensDto
    {
        public TokensDto(Models.Tokens X)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Tokens, TokensDto>());
            Mapper.Map<Models.Tokens, TokensDto>(X, this);
        }

        public string Token { get; set; }
        public BrowserType BrowserType { get; set; }
        public PlatformType PlatformType { get; set; }

        public UserDto User { get; set; }
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