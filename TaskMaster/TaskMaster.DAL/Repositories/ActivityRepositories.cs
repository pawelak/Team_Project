using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.Models;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;


namespace TaskMaster.DAL.Repositories
{
    public class ActivityRepositories : RepoBase<Activity>, IActivityRepositories
    {
        public void Add(ActivityDto dto)
        {
            base.Add(Mapper.Map<Activity>(dto));
        }

        public void Delete(ActivityDto dto)
        {
            base.Delete(Mapper.Map<Activity>(dto));
        }

        public new IList<ActivityDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<ActivityDto>).ToList();
        }

        public new ActivityDto Get(int id)
        {
            return Mapper.Map<ActivityDto>(base.Get(id));
        }

        public IList<ActivityDto> Get(string email)
        {
            var list = GetAll();
            return list.Where(l => l.User.Email.Equals(email)).ToList();
        }

        public void Edit(ActivityDto dto)
        {
            base.Edit(Mapper.Map<Activity>(dto));
        }

    }
}