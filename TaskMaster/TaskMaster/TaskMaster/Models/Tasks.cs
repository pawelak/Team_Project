using SQLite;
using TaskMaster.Enums;

namespace TaskMaster.Models
{
    public class Tasks
    {
        [PrimaryKey,AutoIncrement]
        public int TaskId { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Typ { get; set; }
        public SyncStatus SyncStatus { get; set; }
    }
}
