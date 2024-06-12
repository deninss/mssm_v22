using mssm.Context;
using System.ComponentModel.DataAnnotations;

namespace mssm.Models.TeamModel
{
    public class TeamMembers
    {
        [Key]
        public int Id { get; set; }
        public string TeamID { get; set; }
        public Team Team { get; set; }
        public string UserContextID { get; set; }
        public UserContext UserContext { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
