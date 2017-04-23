using SQLite;

namespace TaskMaster.Models
{
    public class Tasks
    {
        [PrimaryKey,AutoIncrement]
        public int TaskId { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
