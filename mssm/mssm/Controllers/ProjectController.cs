using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mssm.Models.TeamModel;
using mssm.Models.ProjectModel;
using mssm.Services.ITeam;
using mssm.Services.IProject;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.RegularExpressions; 

namespace mssm.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        protected _IProject _project { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectController(_IProject project, IHttpContextAccessor httpContextAccessor)
        {
            _project = project;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProjectModel> item =  await _project.GetAllProject();
            //List<ProjectModel> projectsList = item.ToList();
            //projectsList.RemoveAll(p => !p.TaskModel.Any());
            ViewBag.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); 
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddItemProject()
        {
             ViewBag.IdProject = await _project.CreateProject();
             return PartialView("ItemProject");
        }
        [HttpPost]
        public async Task<IActionResult> AddItemTask(string projectId)
        {
            ViewBag.IdTask = await _project.CreateTask(projectId);
            return PartialView("ItemTask");
        }
        [HttpPost]
        public async Task<IActionResult> AddItemSubtask(string taskId)
        {
            ViewBag.IdSubtask = await _project.CreateSubtask(taskId);
            return PartialView("ItemSubtasks");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProject(string projectId)
        {
            await _project.DeleteProject(projectId); 
            return Json(new { success = true });
        }  
        [HttpPost]
        public async Task<IActionResult> DeleteItemTask(string taskId)
        {
            await _project.DeleteTask(taskId);
            return Json(new { success = true });
        }  
        [HttpPost]
        public async Task<IActionResult> DeleteItemSubtasks(string subtaskId)
        {
            await _project.DeleteSubtasks(subtaskId);
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectModel project)
        {
            await _project.UpdateProject(project);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask([FromBody] TaskModel task)
        {
            await _project.UpdateTask(task);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubtask([FromBody] SubtasksModel subtask)
        {
            if(await _project.UpdateSubtask(subtask)) return Json(new { success = true });
            return Json(new { success = false });
        }
       
        public async Task<IActionResult> ModalWindowSettingProject(string projectId)
        {
            ProjectModel project = await _project.GetSettingProject(projectId);
            IEnumerable<Team> team = await _project.GetSettingProjectTeam();
            var settingModel = new SettingProjectModel
            {
                Project = project,
                Team = team
            };
            return PartialView(settingModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveExecutorDepartment(string ProjectID, string[] Executor, string[] UncheckedDepartments)
        {
            var department = await _project.SaveExecutorDepartment(ProjectID, Executor, UncheckedDepartments);
            if (department != null)return Json(new { success = true });
            else return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> SaveExecutor(string ExecutorId, string TaskId, bool Delete)
        {
            if(await _project.SaveExecutor(ExecutorId, TaskId, Delete)) return Json(new { success = true});
            else return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> StartProject(string projectId,string end)
        {
            if(await _project.StartProject(projectId,end)) return Json(new { success = true});
            else return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> FinalTask(string TaskId)
        {
            if(await _project.FinalTask(TaskId)) return Json(new { success = true});
            else return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> NoTask(string TaskId)
        {
            if(await _project.NoTask(TaskId)) return Json(new { success = true});
            else return Json(new { success = false });
        }
      
        [HttpGet]
        public async Task<IActionResult> UrgencyTask(string TaskId, int selectedValue)
        {
            if (await _project.UrgencyTask(TaskId, selectedValue)) return Json(new { success = true });
            else return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string TaskId)
        {
            if (file == null || file.Length == 0 && TaskId == null)
            {
                return Json(new { success = false });
            }
            else
            {
                bool uploadSuccess = await _project.UploadFile(file, TaskId);
                return Json(new { success = uploadSuccess });
            }
        }
        [HttpGet]
        public async Task<IActionResult> OpenFile(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                return Json(new { success = false, message = "TaskId is required." });
            }
            else
            {
                try
                { 
                    var fileData = await _project.OpenFile(taskId);
                    if (fileData == null)
                    {
                        return Json(new { success = false, message = "The file was not found." });
                    }

                    var fileExtension = Path.GetExtension(taskId).ToLowerInvariant();

                    return File(fileData.bytes, fileData.Expansion, fileData.Name);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
    }
}
