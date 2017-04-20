using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;
    

namespace TaskMaster.DAL.Repositories
{
    public class ActivityRepositories : RepoBase<Activity>
    {
        public ActivityRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Activity, ActivityDto>());
            //Mapper.Map<Models.Activity, ActivityDto>(X, this);
        }


    }
}