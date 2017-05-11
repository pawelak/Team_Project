using System.Collections.Generic;
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
            IList < ActivityDto > list = new List<ActivityDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<ActivityDto>(VARIABLE));
            }
            return list;
        }

        public new ActivityDto Get(int ID)
        {
            return Mapper.Map<ActivityDto>(base.Get(ID));
        }

        public void Edit(ActivityDto dto)
        {
            base.Edit(Mapper.Map<Activity>(dto));
        }

    }
}