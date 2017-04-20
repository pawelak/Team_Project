
using System.Collections.Generic;

namespace TaskMaster.DAL.Models
{
    public class Activity
    {
        public Activity() { }
        public int ActivityId { get; set; }
        public State State { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public ICollection<PartsOfActivity> PartsOfActivity { get; set; }
    }

    public enum State
    {
        Planed,
        Started,
        Paused,
        Ended
    }
}