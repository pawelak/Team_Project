using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TokensRepositories : RepoBase<Tokens>, ITokensRepositories
    {
        public TokensRepositories()
        {
            
        }
        public void Add(TokensDto dto)
        {
            Tokens entity = dto.ToTokens();
            base.Add(entity);
        }

        public void Delete(TokensDto dto)
        {
            Tokens entity = dto.ToTokens();
            base.Delete(entity);
        }

        public IList<TokensDto> GetAll()
        {
            IList<TokensDto> list = new List<TokensDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new TokensDto(VARIABLE));
            }
            return list;
        }

        public TokensDto Get(int ID)
        {
            return new TokensDto(base.Get(ID));
        }

        public void Edit(TokensDto dto)
        {
            Tokens entity = dto.ToTokens();
            base.Edit(entity);
        }
    }
}