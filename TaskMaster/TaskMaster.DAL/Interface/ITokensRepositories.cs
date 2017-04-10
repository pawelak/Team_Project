using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface ITokensRepositories
    {
        void Add(TokensDto dto);
        void Delete(TokensDto dto);
        IList<TokensDto> GetAll();
        TokensDto Get(int ID);
        void Edit(TokensDto dto);
    }
}