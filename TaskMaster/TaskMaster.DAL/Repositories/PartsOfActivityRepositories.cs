using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class PartsOfActivityRepositories : RepoBase<PartsOfActivity>
    {
        public PartsOfActivityRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PartsOfActivity, PartsOfActivityDto>());
            //Mapper.Map<Models.PartsOfActivity, PartsOfActivityDto>(X, this);
        }

    }
}