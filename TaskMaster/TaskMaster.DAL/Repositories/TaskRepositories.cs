using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TaskRepositories : RepoBase<Task>, ITaskRepositories
    {
        public void Add(TaskDto dto)
        {
            base.Add(Mapper.Map<Task>(dto));
        }

        public void Delete(TaskDto dto)
        {
            base.Delete(Mapper.Map<Task>(dto));
        }

        public new IList<TaskDto> GetAll()
        {
            return base.GetAll().Select(Mapper.Map<TaskDto>).ToList();
        }

        public new TaskDto Get(int id)
        {
            return Mapper.Map<TaskDto>(base.Get(id));
        }

        public TaskDto Get(string name)
        {
            return GetAll().FirstOrDefault(v => v.Name.Equals(name));
        }

        public void Edit(TaskDto dto)
        {
            base.Edit(Mapper.Map<Task>(dto));
        }
    }
}