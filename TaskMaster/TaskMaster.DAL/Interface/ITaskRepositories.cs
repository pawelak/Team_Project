using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface ITaskRepositories
    {
        void Add(TaskDto dto);
        void Delete(TaskDto dto);
        IList<TaskDto> GetAll();
        TaskDto Get(int ID);
        void Edit(TaskDto dto);
    }
}