using mssm.Models.TeamModel;

namespace mssm.Models.ProjectModel
{
    public class TaskAndDepartmentModel
    {
        public TaskModel Task { get; set; }
        public IEnumerable<DepartmentModel> Department { get; set; }
    }
}
