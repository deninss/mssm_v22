using mssm.Models.ProjectModel;
using mssm.Models.TeamModel;

namespace mssm.Services.ITeam
{
    public interface _ITeam
    {
        Task<Team> AddTeam(AddTeam group);
        Task<bool> AddTeamStaff(string TeamId, string UserId, int Role);
        Task<bool> AddDepartmentStaff(string TeamId, string UserId, int Role);
        Task<bool> DeleteTeamStaff(int UserId);
        Task<bool> DeleteDepartmentStaff(int UserId);
        Task<string> DeleteDepartment(string DepartmentId);
        Task<bool> DeleteTeam(string TeamId);
        Task<List<Team>> AllTeam();
        Task<Team> GetTeam(string id);
        Task<DepartmentModel> GetDepartment(string id);
        Task<List<DepartmentModel>> AllDepartment();
        Task<DepartmentModel> CreateDepartment(AddDepartment department);
        Task<ProjectModel> GetTeamProject(string Id);
        Task<bool> UpdateRoleTeamStaff(int UserId, int selectedValue, string type);
    }
}
