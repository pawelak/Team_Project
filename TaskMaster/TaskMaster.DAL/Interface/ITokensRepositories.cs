using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface ITokensRepositories
    {
        void Add(TokensDto dto);
        void Attach(TokensDto dto);
        void Delete(TokensDto dto);
        IList<TokensDto> GetAll();
        TokensDto Get(int id);
        IList<TokensDto> Get(string email);
        void Edit(TokensDto dto);
    }
}