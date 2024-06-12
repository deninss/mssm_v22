using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mssm.Context;
using mssm.Models;
using mssm.Models.TeamModel;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace mssm.Services.ISearch
{
    public class _Search : _ISearch
    {
        private readonly UserManager<UserContext> _userManager;
        public ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public _Search(ApplicationDbContext context, UserManager<UserContext> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<SearchModel>> SearchUserAsync(string query)
        {
            var role = await _context.Role.Where(x=>x.TypeRole == "Team").ToListAsync();
            var results = await _userManager.Users.Where(x => EF.Functions.Like(x.UserName, $"%{query}%") && x.Id != _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Select(x => new SearchModel { Id = x.Id, UserName = x.UserName, Role = role}).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<SearchModel>> SearchUserDepartmentAsync(string TeamID, string query)
        {
            try
            {
                var results = await _context.Team
                .Where(v => v.Id == TeamID)
                .SelectMany(x => x.TeamMember
                    .Where(z => EF.Functions.Like(z.UserContext.UserName, $"%{query}%") && z.UserContextID != _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .Select(u => new SearchModel
                    {
                        Id = u.UserContextID,
                        UserName = u.UserContext.UserName,
                        Role = _context.Role.Where(r => r.TypeRole == "Department").ToList(),
                    }))
                .ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
           
        }

        public async Task<IEnumerable<SearchModel>> SearchTeamStaff(string query,string TeamId)
        {
            var role = await _context.Role.Where(x => x.TypeRole == "Team").ToListAsync();
            var results = await _userManager.Users
                .Where(x =>
                    EF.Functions.Like(x.UserName, $"%{query}%") &&
                    x.Id != _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) &&
                    !_context.TeamMembers.Any(tm => tm.UserContextID == x.Id && tm.TeamID == TeamId)
                ).Select(x => new SearchModel { Id = x.Id, UserName = x.UserName, Role = role }).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<SearchModel>> SearchDepartmentStaff(string query, string TeamId)
        {
            var amId = await _context.Team
               .Where(dm => dm.DepartmentModels.Any(t => t.Id == TeamId))
               .Select(dm => dm.Id)
               .FirstOrDefaultAsync();
            var role = await _context.Role.Where(x => x.TypeRole == "Department").ToListAsync();
            var results = await _userManager.Users
            .Where(x =>
                EF.Functions.Like(x.UserName, $"%{query}%") &&
                x.Id != _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) &&
                x.TeamMember.Any(d => d.TeamID == amId) &&
                !_context.DepartmentMembers.Any(d => d.DepartmentID == TeamId && d.UserContextID == x.Id)
            )
            .Select(x => new SearchModel { Id = x.Id, UserName = x.UserName, Role = role })
            .ToListAsync();

            return results;
        }
    }
}
