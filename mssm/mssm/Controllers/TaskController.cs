using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mssm.Models.TeamModel;
using mssm.Models.ProjectModel;
using mssm.Services.IProject;
using mssm.Services.ITask;
using Newtonsoft.Json;
using System.Security.Claims;

namespace mssm.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        protected _ITask _task { get; }
        public TaskController(_ITask task)
        {
            _task = task;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<TaskModel> item = await _task.GetAllTask();
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> EndTask(string TaskId)
        {
            if(await _task.EndTask(TaskId)) return Json(new { success = true });
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
                bool uploadSuccess = await _task.UploadFile(file, TaskId);
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
                    var fileData = await _task.OpenFile(taskId);
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
