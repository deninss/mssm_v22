using mssm.Models;

namespace mssm.Services.ISearch
{
    public interface _ISearch
    {
        Task<IEnumerable<SearchModel>> SearchUserAsync(string query);
        Task<IEnumerable<SearchModel>> SearchTeamStaff(string query, string TeamId);
        Task<IEnumerable<SearchModel>> SearchDepartmentStaff(string query, string TeamId);
        Task<IEnumerable<SearchModel>> SearchUserDepartmentAsync(string TeamID, string query);
    }
}
