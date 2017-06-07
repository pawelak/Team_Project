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
            var result = Mapper.Map<Activity>(dto);
            base.Add(result);
        }
        public void Attach(ActivityDto dto)
        {
            var result = Mapper.Map<Activity>(dto);
            base.Attach(result);
        }
        public void Delete(ActivityDto dto)
        {
            var result = Mapper.Map<Activity>(dto);
            base.Delete(result);
        }
        public new IList<ActivityDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<ActivityDto>);
            return result.ToList();
        }
        public new ActivityDto Get(int id)
        {
            var result = Mapper.Map<ActivityDto>(base.Get(id));
            return result;
        }
        public IList<ActivityDto> Get(string email)
        {
            var result = GetAll().Where(v => v.User.Email.Equals(email));
            return result.ToList();
        }
        public void Edit(ActivityDto dto)
        {
            var result = Mapper.Map<Activity>(dto);
            base.Edit(result, p=>p.UserId);
        }
    }
}