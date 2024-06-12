using mssm.Context;
using mssm.Models.TeamModel;
using System.ComponentModel.DataAnnotations;

namespace mssm.Models.ProjectModel
{
    public class ProjectModel
    {
        public string Id { get; set; }
        public string IdDirector { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public ICollection<DepartmentModel> DepartmentModel { get; set; }
        public ICollection<TaskModel>? TaskModel { get; set; }

        public ProjectModel()
        {
            DepartmentModel = new List<DepartmentModel>();
            TaskModel = new List<TaskModel>();
        }
    }
}
