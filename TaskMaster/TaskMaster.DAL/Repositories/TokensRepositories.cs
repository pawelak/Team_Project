using System.Collections.Generic;
using System.Linq;
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

        public new IList<TokensDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<TokensDto>).ToList();
        }

        public new TokensDto Get(int id)
        {
            return Mapper.Map<TokensDto>(base.Get(id));
        }

        public IList<TokensDto> Get(string email)
        {
            var list = GetAll();
            return list.Where(l => l.User.Email.Equals(email)).ToList();
        }

        public void Edit(TokensDto dto)
        {
            base.Edit(Mapper.Map<Tokens>(dto),"TokensId");
        }
    }
}