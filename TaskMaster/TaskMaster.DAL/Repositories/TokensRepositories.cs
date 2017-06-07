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
            var result = Mapper.Map<Tokens>(dto);
            result.UserId = result.User.UserId;
            result.User = null;
            base.Add(result);
        }
        public void Delete(TokensDto dto)
        {
            var result = Mapper.Map<Tokens>(dto);
            base.Delete(result);
        }
        public new IList<TokensDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<TokensDto>);
            return result.ToList();
        }
        public new TokensDto Get(int id)
        {
            var result = Mapper.Map<TokensDto>(base.Get(id));
            return result;
        }
        public IList<TokensDto> Get(string email)
        {
            var result = GetAll().Where(l => l.User.Email.Equals(email));
            return result.ToList();
        }
        public void Edit(TokensDto dto)
        {
            var result = Mapper.Map<Tokens>(dto);
            base.Edit(result);
        }
    }
}