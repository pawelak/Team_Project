namespace TaskMaster.ModelsDto
{
    public class PartsOfActivityDto
    {
        public int PartId { get; set; }
        public int ActivityId { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public string Duration { get; set; }
        public SyncStatusType SyncStatus { get; set; }
    }
}
