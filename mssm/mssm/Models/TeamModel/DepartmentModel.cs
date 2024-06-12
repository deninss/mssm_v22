using mssm.Context;

namespace mssm.Models.TeamModel
{
    public class DepartmentModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Team> Team { get; set; }
        public virtual ICollection<ProjectModel.ProjectModel>? ProjectModel { get; set; }
        public ICollection<DepartmentMembers> DepartmentMembers { get; set; }
        public DepartmentModel()
        {
            Team = new List<Team>();
            ProjectModel = new List<ProjectModel.ProjectModel>();
            DepartmentMembers = new List<DepartmentMembers>();
        }
    }
}
