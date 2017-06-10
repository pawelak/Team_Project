using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IFavoritesRepositories
    {
        int Add(FavoritesDto dto);
        void Delete(FavoritesDto dto);
        IList<FavoritesDto> GetAll();
        FavoritesDto Get(int id);
        IList<FavoritesDto> Get(string email);
        void Edit(FavoritesDto dto);
    }
}