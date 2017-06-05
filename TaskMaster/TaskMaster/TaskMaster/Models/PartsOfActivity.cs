using SQLite;

namespace TaskMaster.Models
{
    public class PartsOfActivity
    {
        [PrimaryKey,AutoIncrement]
        public int PartId { get; set; }
        public int ActivityId { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public string Duration { get; set; }
    }
}
