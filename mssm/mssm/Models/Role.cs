using mssm.Models.TeamModel;

namespace mssm.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string NameRole { get; set; }
        public string TypeRole { get; set; }
        public ICollection<TeamMembers> Members { get; set; }   
        public Role()
        {
            Members = new HashSet<TeamMembers>();
        }
    }
}
