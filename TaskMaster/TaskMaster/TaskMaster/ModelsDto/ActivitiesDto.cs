﻿using TaskMaster.Enums;

namespace TaskMaster.ModelsDto
{
    public class ActivitiesDto
    {
        public int ActivityId { get; set; }
        public string Guid { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int GroupId { get; set; }
        public StatusType Status { get; set; }
        public SyncStatus SyncStatus { get; set; }
    }
}
