using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mssm.Models.TeamModel;
using mssm.Services.ITeam;
using mssm.Services.ISearch;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using mssm.Models.ProjectModel;
using mssm.Services.IProject;

namespace mssm.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        protected _ISearch _search { get; }
        protected _ITeam _team {  get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamController(_ISearch search, _ITeam team, IHttpContextAccessor httpContextAccessor)
        { 
            _search = search;
            _team = team;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> item = await _team.AllTeam();
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> ItemTeam(string? id)
        {
            Team item = await _team.GetTeam(id);
            ViewBag.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return PartialView("_Team", item);
        }
        [HttpGet] 
        public async Task<IActionResult> GetDepartment(string? id)
        {
            DepartmentModel item = await _team.GetDepartment(id);
            ViewBag.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return PartialView("Departments", item);
            
        } 
        public IActionResult ModalWindowTeam()
        {
            return PartialView("ModalWindowTeam");
        }
        public IActionResult ModalWindowDepartment(string Id)
        {
            ViewBag.IdTeam = Id;
            return PartialView();
        }  
        public async Task<IActionResult> ModalWindowTeamGetProjectAsync(string Id)
        {
            ProjectModel project = await _team.GetTeamProject(Id);
            ViewBag.IdTeam = Id;
            return PartialView(project);
        }
    
        [HttpPost]
        public async Task<IActionResult> SearchTeam(string query)
        {
            var results = await _search.SearchUserAsync(query);
            return PartialView("_SearchResults", results);
        } 
        [HttpPost]
        public async Task<IActionResult> SearchTeamStaff(string query, string TeamId)
        {
            var results = await _search.SearchTeamStaff(query, TeamId);
            return PartialView("_SearchResultsTeam", results);
        }  
        [HttpPost]
        public async Task<IActionResult> SearchDepartmentStaff(string query, string TeamId)
        {
            var results = await _search.SearchDepartmentStaff(query, TeamId);
            return PartialView("_SearchResultsTeam", results);
        }  
        [HttpGet]
        public async Task<IActionResult> SearchDepartment(string TeamID,string query)
        {
            var results = await _search.SearchUserDepartmentAsync(TeamID,query);
            return PartialView("_SearchResults", results);
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveDepartments(AddDepartment department)
        {
            if (ModelState.IsValid)
            {
               var dep = await _team.CreateDepartment(department);
               return Json(new { success = true, newDepId = dep.Id, newDepName = dep.Name});
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> AddTeamStaff(string TeamId, string UserId, int Role)
        {
            if (ModelState.IsValid)
            {
                await _team.AddTeamStaff(TeamId, UserId, Role);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartmentStaff(string TeamId, string UserId, int Role)
        {
            if (ModelState.IsValid)
            {
                await _team.AddDepartmentStaff(TeamId, UserId, Role);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTeamStaff(int UserId)
        {
            if (ModelState.IsValid)
            {
                await _team.DeleteTeamStaff(UserId);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
            
        [HttpPost]
        public async Task<IActionResult> DeleteDepartmentStaff(int UserId)
        {
            if (ModelState.IsValid)
            {
                await _team.DeleteDepartmentStaff(UserId);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(string DepartmentId)
        {
            if (ModelState.IsValid)
            {
                var teamId = await _team.DeleteDepartment(DepartmentId);
                if (teamId == null) return await ItemTeam(teamId);
                else NoContent();
            }
            return Json(new { success = false });
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteTeam(string TeamId)
        {
            if (ModelState.IsValid)
            {
                var teamId = await _team.DeleteTeam(TeamId);
                if (teamId == true) return Json(new { success = true });
                else return Json(new { success = false });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTeam(AddTeam addGroup)
        {
            if(ModelState.IsValid)
            {
               var team =  await _team.AddTeam(addGroup);
               return Json(new { success = true, newTeamsId = team.Id , newTeamsName = team.Name});
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRoleTeamStaff(int UserId, int selectedValue,string type)
        {
            if (await _team.UpdateRoleTeamStaff(UserId, selectedValue,type)) return Json(new { success = true });
            else return Json(new { success = false });
        }
    }
}
