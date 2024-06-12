using mssm.Context;

namespace mssm.Models.TeamModel
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TeamMembers>? TeamMember { get; set; }
     
        public ICollection<DepartmentModel>? DepartmentModels { get; set; }
        public Team()
        {
            TeamMember = new List<TeamMembers>();
   
            DepartmentModels = new List<DepartmentModel>();
        }
    }
}
