using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface IFavoritesRepositories
    {
        void Add(FavoritesDto dto);
        void Attach(FavoritesDto dto);
        void Delete(FavoritesDto dto);
        IList<FavoritesDto> GetAll();
        FavoritesDto Get(int id);
        IList<FavoritesDto> Get(string email);
        void Edit(FavoritesDto dto);
    }
}