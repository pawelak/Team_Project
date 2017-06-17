using System.Collections.Generic;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.DAL.Interface
{
    public interface ITaskRepositories
    {
        int Add(TaskDto dto);
        void Delete(TaskDto dto);
        IList<TaskDto> GetAll();
        TaskDto Get(int id);
        TaskDto Get(string name);
        void Edit(TaskDto dto);
    }
}