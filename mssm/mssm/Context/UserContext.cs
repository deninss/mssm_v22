using Microsoft.AspNetCore.Identity;
using mssm.Models.TeamModel;
using mssm.Models.ProjectModel;

namespace mssm.Context
{
    public class UserContext : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public string? Surname { get; set; }
        [PersonalData]
        public string? Patronymic { get; set; }
        public virtual ICollection<TeamMembers>? TeamMember { get; set; }
        public virtual ICollection<ExecutorTask>? ExecutorTask { get; set; }
        //public ICollection<DepartmentModel>? DepartmentModels { get; set; }
        public UserContext()
        {
            TeamMember = new List<TeamMembers>();
            ExecutorTask = new List<ExecutorTask>();
            //DepartmentModels = new List<DepartmentModel>();
        }
    }
}
