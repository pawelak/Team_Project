using TaskMaster.Enums;

namespace TaskMaster.ModelsDto
{
    public class TasksDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Typ { get; set; }
        public SyncStatus SyncStatus { get; set; }
    }
}
