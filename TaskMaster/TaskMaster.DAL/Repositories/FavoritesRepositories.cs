using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class FavoritesRepositories : RepoBase<Favorites>, IFavoritesRepositories
    {
        public void Add(FavoritesDto dto)
        {
            var result = Mapper.Map<Favorites>(dto);
            result.UserId = result.User.UserId;
            result.User = null;
            result.TaskId = result.Task.TaskId;
            result.Task = null;
            base.Add(result);
        }
        public void Delete(FavoritesDto dto)
        {
            var obj = Mapper.Map<Favorites>(dto);
            var result = Db.Favorites.Find(obj.FavoritesId);
            base.Delete(result);
        }
        public new IList<FavoritesDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<FavoritesDto>);
            return result.ToList();
        }
        public new FavoritesDto Get(int id)
        {
            var result = Mapper.Map<FavoritesDto>(base.Get(id));
            return result;
        }
        public IList<FavoritesDto> Get(string email)
        {
            var result = GetAll().Where(v => v.User.Email.Equals(email));
            return result.ToList();
        }
        public void Edit(FavoritesDto dto)
        {
            var obj = Mapper.Map<Favorites>(dto);
            var result = Db.Favorites.Find(obj.FavoritesId);
            result.UserId = obj.UserId;
            result.TaskId = obj.TaskId;
            base.Edit(result);
        }
    }
}