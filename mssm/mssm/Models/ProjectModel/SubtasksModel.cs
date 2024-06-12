namespace mssm.Models.ProjectModel
{
    public class SubtasksModel
    {
        public string Id { get; set; }
        public string? Description { get; set; }
        public string TaskId { get; set; }
        public TaskModel Task { get; set; }
    }
}
