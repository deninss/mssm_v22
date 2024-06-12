using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mssm.Context;
using mssm.Models;
using mssm.Models.ProjectModel;
using mssm.Models.TeamModel;
using mssm.Services.ITeam;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;

namespace mssm.Services.ITeam
{
    public class _Team : _ITeam
    {
        public ApplicationDbContext _context;
        private readonly UserManager<UserContext> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _Team(ApplicationDbContext context, UserManager<UserContext> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Team> AddTeam(AddTeam addgroup)
        {
            try
            {
                Team team = new Team();
                team.Id = Guid.NewGuid().ToString(); 
                team.Name = addgroup.Name;

                TeamMembers Director = new TeamMembers();
                Director.TeamID = team.Id;
                Director.UserContextID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Director.RoleID = 1;
                team.TeamMember.Add(Director);

                foreach (var user in addgroup.Participants)
                {
                    TeamMembers members = new TeamMembers();
                    members.TeamID = team.Id;
                    members.UserContextID = user.Id;
                    members.RoleID = user.Role;
                    team.TeamMember.Add(members);
                }
                 
                await _context.Team.AddAsync(team); 
                await _context.SaveChangesAsync();  

                return team;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Team>> AllTeam()
        {
            string id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = _context.Team.Where(t => t.TeamMember.Any(tm => tm.UserContextID == id)).ToList();
            if (team != null) return team;
            else return null; 
        }

        public async Task<DepartmentModel> CreateDepartment(AddDepartment department)
        {
            try
            {
                DepartmentModel model = new DepartmentModel();
                model.Id = Guid.NewGuid().ToString();
                model.Name = department.Name;
                model.Description = department.Description;
                Team team = _context.Team.Where(x=>x.Id == department.TeamId).FirstOrDefault();  
                model.Team.Add(team);

                DepartmentMembers Director = new DepartmentMembers();
                Director.DepartmentID = model.Id;
                Director.UserContextID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Director.RoleID = 2;
                model.DepartmentMembers.Add(Director);

                foreach (var user in department.Participants)
                {
                    DepartmentMembers members = new DepartmentMembers();
                    members.DepartmentID = model.Id;
                    members.UserContextID = user.Id;
                    members.RoleID = user.Role;
                    model.DepartmentMembers.Add(members);
                }
                await _context.Department.AddAsync(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
        }

        public async Task<Team> GetTeam(string id)
        {
            try
            {
                var meID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var team = await _context.Team
                    .Where(x => x.Id == id)
                    .Include(t => t.DepartmentModels.Where(d=>d.DepartmentMembers.Any(v=>v.UserContextID == meID)))
                    .ThenInclude(d=>d.ProjectModel).ThenInclude(s=>s.TaskModel)
                    .Include(t => t.TeamMember)
                        .ThenInclude(tm => tm.UserContext)
                    .Include(t => t.TeamMember)
                        .ThenInclude(tm => tm.Role) 
                    .FirstOrDefaultAsync();
             
                return team;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error GetTeam: {ex.Message}");
                throw;
            }
        }

        public async Task<List<DepartmentModel>> AllDepartment()
        {
            //return _context.Department.Where(g => g.Team == id).ToList();
            throw new NotImplementedException();
        }

        public async Task<bool> AddTeamStaff(string TeamId, string UserId, int Role)
        {
            try
            {
                TeamMembers user = new TeamMembers();
                user.TeamID = TeamId;
                user.UserContextID = UserId;
                user.RoleID = Role;

                await _context.TeamMembers.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error GetTeam: {ex.Message}");
                throw;
            } 
        }

        public async Task<bool> DeleteTeamStaff(int userId)
        {
            try
            { 
                var teamMember = await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == userId);
                if (teamMember == null) return false;
                var departmentModelIds = await _context.Department
                    .Where(dm => dm.Team.Any(t => t.Id == teamMember.TeamID))
                    .Select(dm => dm.Id)
                    .ToListAsync();
                var departmentMembersToRemove = await _context.DepartmentMembers
                    .Where(dm => departmentModelIds.Contains(dm.DepartmentID) && dm.UserContextID == teamMember.UserContextID)
                    .ToListAsync();

                _context.DepartmentMembers.RemoveRange(departmentMembersToRemove);
                _context.TeamMembers.Remove(teamMember);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error DeleteTeamStaff: {ex.Message}");
                throw;
            }
        }

        public async Task<DepartmentModel> GetDepartment(string id)
        {
            try
            {
                var meID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var team = await _context.Department
                  .Where(x => x.Id == id)
                  .Include(t => t.DepartmentMembers)
                      .ThenInclude(tm => tm.UserContext)
                  .Include(t => t.DepartmentMembers)
                      .ThenInclude(tm => tm.Role)
                    .Include(s=>s.ProjectModel).ThenInclude(s => s.TaskModel)
                  .FirstOrDefaultAsync();
                return team;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error GetTeam: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteDepartmentStaff(int UserId)
        {
            try
            {
                var user = await _context.DepartmentMembers.FirstOrDefaultAsync(x => x.Id == UserId);
                _context.DepartmentMembers.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error GetTeam: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddDepartmentStaff(string TeamId, string UserId, int Role)
        {
            try
            {
                DepartmentMembers user = new DepartmentMembers();
                user.DepartmentID = TeamId;
                user.UserContextID = UserId;
                user.RoleID = Role;

                await _context.DepartmentMembers.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error GetTeam: {ex.Message}");
                throw;
            }
        }

        public async Task<string> DeleteDepartment(string DepartmentId)
        {
            try
            {
                var department = await _context.Department
                  .Include(d => d.Team)  
                  .SingleOrDefaultAsync(d => d.Id == DepartmentId);
                string teamIds = department.Team.Select(t => t.Id).FirstOrDefault();
                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
                return teamIds;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error DeleteDepartment: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTeam(string TeamId)
        {
            try
            {
                var team = await _context.Team.Where(x => x.Id == TeamId).Include(c=>c.DepartmentModels).FirstOrDefaultAsync();
                if (team != null)
                {
                    _context.Team.Remove(team);

                    var departmentId = team.DepartmentModels.Select(x => x.Id).ToList();
                    if (departmentId != default)
                    {
                        foreach (var item in departmentId)
                        {
                            var department = await _context.Department.FindAsync(item);
                            if (department != null)
                            {
                                _context.Department.Remove(department);
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error DeleteTeam: {ex.Message}");
                throw;
            }
        }

        public async Task<ProjectModel> GetTeamProject(string Id)
        { 
            try
            {
                var project = await _context.Project.Where(x=>x.Id == Id)
                    .Include(c=>c.TaskModel)
                    .ThenInclude(v=>v.SubtasksModel)
                    .Include(y => y.TaskModel)
                    .ThenInclude(i => i.Executor).ThenInclude(b=>b.user).FirstOrDefaultAsync();
                return project;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error GetTeamProject: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UpdateRoleTeamStaff(int UserId, int selectedValue, string type)
        {
            try
            {
                if(type == "team")
                {
                    var user = await _context.TeamMembers.FindAsync(UserId);
                    user.RoleID = selectedValue;
                    _context.TeamMembers.Update(user);
                }
                else if (type == "department")
                {
                    var user = await _context.DepartmentMembers.FindAsync(UserId);
                    user.RoleID = selectedValue;
                    _context.DepartmentMembers.Update(user);
                }
               
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UploadFile: {ex.Message}");
                throw;
            }
        }
    }
}
