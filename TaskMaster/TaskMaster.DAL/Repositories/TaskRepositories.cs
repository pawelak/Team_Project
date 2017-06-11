using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.Context;
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
            result.Activities = null;
            result.Favorites = null;
            base.Add(result);
        }
        public void Delete(TaskDto dto)
        {
            var obj = Mapper.Map<Task>(dto);
            var result = Db.Task.Find(obj.TaskId);
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
            var obj = Mapper.Map<Task>(dto);
            var result = Db.Task.Find(obj.TaskId);
            Db.Task.Attach(result);
            result.Description = obj.Description;
            result.Name = obj.Name;
            result.Type = obj.Type;
            base.Edit(result);
        }
    }
}