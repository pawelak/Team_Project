using System.Collections.Generic;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TokensRepositories : RepoBase<Tokens>, ITokensRepositories
    {
        public void Add(TokensDto dto)
        {
            base.Add(Mapper.Map<Tokens>(dto));
        }

        public void Delete(TokensDto dto)
        {
            base.Delete(Mapper.Map<Tokens>(dto));
        }

        public IList<TokensDto> GetAll()
        {
            IList<TokensDto> list = new List<TokensDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<TokensDto>(VARIABLE));
            }
            return list;
        }

        public new TokensDto Get(int ID)
        {
            return Mapper.Map<TokensDto>(base.Get(ID));
        }

        public void Edit(TokensDto dto)
        {
            base.Edit(Mapper.Map<Tokens>(dto));
        }
    }
}