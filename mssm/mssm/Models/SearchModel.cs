using mssm.Models.TeamModel;

namespace mssm.Models
{
    public class SearchModel 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<Role> Role { get; set; }
    }
}
