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
            var result = Mapper.Map<Task>(dto);
            base.Add(result);
        }
        public void Attach(TaskDto dto)
        {
            var result = Mapper.Map<Task>(dto);
            base.Attach(result);
        }
        public void Delete(TaskDto dto)
        {
            var result = Mapper.Map<Task>(dto);
            base.Delete(result);
        }
        public new IList<TaskDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<TaskDto>);
            return result.ToList();
        }
        public new TaskDto Get(int id)
        {
            var result = Mapper.Map<TaskDto>(base.Get(id));
            return result;
        }
        public TaskDto Get(string name)
        {
            var result = GetAll().FirstOrDefault(v => v.Name.Equals(name));
            return result;
        }
        public void Edit(TaskDto dto)
        {
            var result = Mapper.Map<Task>(dto);
            base.Edit(result, p => p.TaskId);
        }
    }
}