using mssm.Context;
using mssm.Models.TeamModel;
using System.ComponentModel.DataAnnotations;

namespace mssm.Models.ProjectModel
{
    public class TaskModel
    { 
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public File? File { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? TypeId { get; set; }
        public string ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        public ICollection<SubtasksModel>? SubtasksModel { get; set; }
        public ICollection<ExecutorTask>? Executor { get; set; }
        public TaskModel()
        {
            SubtasksModel = new List<SubtasksModel>();
            Executor = new List<ExecutorTask>();
        }
    }
}
