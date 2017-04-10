using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class TaskRepositories : RepoBase<Task>, ITaskRepositories
    {
        public TaskRepositories()
        {
            
        }
        public void Add(TaskDto dto)
        {
            Task entity = dto.ToTask();
            base.Add(entity);
        }

        public void Delete(TaskDto dto)
        {
            Task entity = dto.ToTask();
            base.Delete(entity);
        }

        public IList<TaskDto> GetAll()
        {
            IList<TaskDto> list = new List<TaskDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new TaskDto(VARIABLE));
            }
            return list;
        }

        public TaskDto Get(int ID)
        {
            return new TaskDto(base.Get(ID));
        }

        public void Edit(TaskDto dto)
        {
            Task entity = dto.ToTask();
            base.Edit(entity);
        }
    }
}