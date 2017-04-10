using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TokensRepositories : RepoBase<Tokens>
    {
        public TokensRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.Tokens, TokensDto>());
           // Mapper.Map<Models.Tokens, TokensDto>(X, this);
        }

    }
}