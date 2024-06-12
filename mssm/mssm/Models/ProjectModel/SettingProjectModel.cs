using mssm.Models.TeamModel;

namespace mssm.Models.ProjectModel
{
    public class SettingProjectModel
    {
        public ProjectModel Project { get; set; }
        public IEnumerable<Team> Team { get; set; }
    }
}
