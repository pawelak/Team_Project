using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using TaskMaster.DAL.Models;
using TaskMaster.DAL.DTOModels;


namespace TaskMaster.DAL.Repositories
{
    public class ActivityRepositories : RepoBase<Activity>, IActivityRepositories
    {
        public ActivityRepositories()
        {
            
        }

        public void Add(ActivityDto dto)
        {
            Activity entity = dto.ToActivity();
            base.Add(entity);
        }

        public void Delete(ActivityDto dto)
        {
            Activity entity = dto.ToActivity();
            base.Delete(entity);
        }

        public IList<ActivityDto> GetAll()
        {
            IList < ActivityDto > list = new List<ActivityDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new ActivityDto(VARIABLE));
            }
            return list;
        }

        public ActivityDto Get(int ID)
        {
            return new ActivityDto(base.Get(ID));
        }

        public void Edit(ActivityDto dto)
        {
            Activity entity = dto.ToActivity();
            base.Edit(entity);
        }

    }
}