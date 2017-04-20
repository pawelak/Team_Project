using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class GroupRepositories : RepoBase<Group>
    {
        public GroupRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Group, GroupDto>());
           // Mapper.Map<Models.Group, GroupDto>(X, this);
        }


    }
}