using System.Collections.Generic;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TaskRepositories : RepoBase<Task>, ITaskRepositories
    {
        public TaskRepositories()
        {
            Mapper.Initialize(ctg => ctg.AddProfile(new MapperProfil()));
        }
        public void Add(TaskDto dto)
        {
            base.Add(Mapper.Map<Task>(dto));
        }

        public void Delete(TaskDto dto)
        {
            base.Delete(Mapper.Map<Task>(dto));
        }

        public IList<TaskDto> GetAll()
        {
            IList<TaskDto> list = new List<TaskDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(Mapper.Map<TaskDto>(VARIABLE));
            }
            return list;
        }

        public new TaskDto Get(int ID)
        {
            return Mapper.Map<TaskDto>(base.Get(ID));
        }

        public void Edit(TaskDto dto)
        {
            base.Edit(Mapper.Map<Task>(dto));
        }
    }
}