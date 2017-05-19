namespace TaskMaster.ModelsDto
{
    public class TasksDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Typ { get; set; }
        public SyncStatusType SyncStatus { get; set; }
    }
}
