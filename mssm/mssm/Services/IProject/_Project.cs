using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mssm.Context;
using mssm.Models.ProjectModel;
using mssm.Models.TeamModel;
using System.Security.Claims;

namespace mssm.Services.IProject
{
    public class _Project : _IProject
    {
        public ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _Project(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<ProjectModel>> GetAllProject()
        {
            try
            {
                string id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var project = await _context.Project
                  .Where(x => x.IdDirector == id || x.DepartmentModel
                      .Any(department => department.DepartmentMembers
                          .Any(member => member.RoleID != 8 && member.UserContextID == id)))
                  .Include(p => p.TaskModel)
                  .ThenInclude(t => t.SubtasksModel)
                  .Include(d => d.DepartmentModel)
                  .ThenInclude(a => a.DepartmentMembers)
                  .ThenInclude(e => e.UserContext)
                  .Include(y => y.TaskModel)
                  .ThenInclude(i => i.Executor)
                    .Include(ys => ys.TaskModel)
                  .ThenInclude(iws => iws.File)
                  .ToListAsync();
                var userRole = project.SelectMany(p => p.DepartmentModel)
                    .SelectMany(d => d.DepartmentMembers)
                    .FirstOrDefault(m => m.UserContextID == id)?.RoleID;
                if (userRole != null)
                {
                    project = project.Where(p =>
                    {
                        if (userRole == 6)
                        {
                            if (project.Any(x=>x.TaskModel.Any(t => t.Status == 1 || t.Status == 2)))
                            {
                                p.TaskModel = p.TaskModel.Where(t => t.Status == 1 || t.Status == 2).ToList();
                                return true;
                            }
                            else return false;
                        }
                        else if (userRole == 2 || userRole == 4) return true;
                        else return false;
                    }).ToList();
                }
                return project; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating GetAllProject: {ex.Message}");
                throw;
            }
        }
        public async Task<string> CreateProject()
        {
            try
            {
                string id = Guid.NewGuid().ToString();
                ProjectModel projectModel = new ProjectModel()
                {
                    Id = id,
                    IdDirector =  _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Status = 0,
                };
                _context.Project.Add(projectModel);
                await _context.SaveChangesAsync();
                return projectModel.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
        }
        public async Task<string> CreateTask(string projectId)
        {
            string id = Guid.NewGuid().ToString();
            TaskModel taslModel = new TaskModel()
            {
                Id = id,
                ProjectId = projectId,
                Status = 0
            };

            try
            {
                _context.Task.Add(taslModel);
                await _context.SaveChangesAsync();
                return taslModel.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating task: {ex.Message}");
                throw;
            }
        }
        public async Task<string> CreateSubtask(string TaskId)
        {
            string id = Guid.NewGuid().ToString();
            SubtasksModel subtasksModel = new SubtasksModel()
            {
                Id = id,
                TaskId = TaskId
            };

            try
            {
                _context.Subtasks.Add(subtasksModel);
                await _context.SaveChangesAsync();
                return subtasksModel.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating task: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateProject(ProjectModel project)
        {
            try
            {
                var upProject = await _context.Project.FindAsync(project.Id);
                upProject.Name = project.Name;
                upProject.Description = project.Description;
                upProject.StartDate = project.StartDate;
                upProject.EndDate = project.EndDate;
                _context.Project.Update(upProject);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        } 
        public async Task UpdateTask(TaskModel task)
        {
            try
            {
                var upTask= await _context.Task.FindAsync(task.Id);
                upTask.Name = task.Name;
                upTask.Description = task.Description;
                upTask.File = task.File;
                upTask.StartDate = task.StartDate;
                upTask.EndDate = task.EndDate;
                _context.Task.Update(upTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        } 
        public async Task<bool> UpdateSubtask(SubtasksModel subtask)
        {
            try
            {
                var upSubtask = await _context.Subtasks.FindAsync(subtask.Id);
                upSubtask.Description = subtask.Description;
                _context.Subtasks.Update(upSubtask);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteProject(string projectId)
        {
            try
            {
                var executorTasks = await _context.ExecutorTask.Where(et => et.Task.ProjectId == projectId).ToListAsync();
                _context.ExecutorTask.RemoveRange(executorTasks);

                var project = await _context.Project.FindAsync(projectId);
                _context.Project.Remove(project);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteTask(string taskId)
        {
            try
            {
                var executorTasks = await _context.ExecutorTask.Where(et => et.Task.Id == taskId).ToListAsync();
                _context.ExecutorTask.RemoveRange(executorTasks);

                var Task = await _context.Task.FindAsync(taskId);
                _context.Task.Remove(Task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteSubtasks(string subtasksId)
        {
            try
            {
                var Subtasks = await _context.Subtasks.FindAsync(subtasksId);
                _context.Subtasks.Remove(Subtasks);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }

        public async Task<ProjectModel> GetSettingProject(string projectId)
        {
            try
            {
                 var project = await _context.Project.Where(x=>x.Id == projectId)
                    .Include(c=>c.TaskModel).FirstOrDefaultAsync();
                return project;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetSettingProject: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Team>> GetSettingProjectTeam()
        {
            try
            {
                string id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var teams = await _context.TeamMembers
                    .Where(x => x.UserContext.Id == id)
                    .Include(c => c.Team)
                    .ThenInclude(t => t.DepartmentModels).ThenInclude(s=>s.ProjectModel)
                    .Select(c => c.Team)
                    .ToListAsync();
                return teams;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetSettingProjectTeam: {ex.Message}");
                throw;
            }
        }

        public async Task<List<DepartmentModel>> SaveExecutorDepartment(string ProjectID,string[] Executor, string[] UncheckedDepartments)
        {
            try
            {
                var project = await _context.Project.FindAsync(ProjectID);
                if (project == null)
                {
                    throw new Exception($"Project with ID {ProjectID} not found.");
                }

                var departments = await _context.Department
                 .Where(d => Executor.Contains(d.Id)).Include(x=>x.DepartmentMembers.Where(d=>d.RoleID == 8)).ThenInclude(w=>w.UserContext)
                 .ToListAsync();

                foreach (var department in departments)
                {
                    _context.Entry(department).Collection(d => d.ProjectModel).Load();
                }

                foreach (var department in departments)
                {
                    if (!department.ProjectModel.Any(p => p.Id == project.Id))
                    {
                        department.ProjectModel.Add(project);
                    }
                }
                foreach (var departmentId in UncheckedDepartments)
                {
                    var department = _context.Department.Include(d => d.ProjectModel).FirstOrDefault(d => d.Id == departmentId);
                    if (department != null)
                    {
                        department.ProjectModel.Remove(project);
                    }
                }

                await _context.SaveChangesAsync();
                return departments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SaveExecutor: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> SaveExecutor(string ExecutorId, string TaskId, bool Delete)
        {
            try
            {
                if (ExecutorId != null && TaskId != null)
                {
                    var task = await _context.Task.Where(t => t.Id == TaskId).Include(s=>s.Executor).ThenInclude(r=>r.user).FirstOrDefaultAsync();
                    if (Delete != true)
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ExecutorId);
                        if (user != null && task != null)
                        {
                            if (task.Executor == null || !task.Executor.Any(e => e.user != null && e.user.Id == user.Id))
                            {
                                var executor = new ExecutorTask
                                {
                                    id = Guid.NewGuid().ToString(),
                                    user = user
                                };

                                if (task.Executor == null)
                                {
                                    task.Executor = new List<ExecutorTask>();
                                }

                                task.Executor.Add(executor);
                                await _context.SaveChangesAsync();
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        var executorToRemove = task.Executor.FirstOrDefault(e => e.user.Id == ExecutorId);
                        if (executorToRemove != null)
                        {
                            task.Executor.Remove(executorToRemove);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SaveExecutor: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> StartProject(string projectId, string end)
        {
            try
            {
                if(projectId != null)
                {
                    var project = await _context.Project.Where(c=>c.Id == projectId).Include(d=>d.DepartmentModel)
                        .Include(v=>v.TaskModel).ThenInclude(d=>d.SubtasksModel)
                        .Include(v => v.TaskModel).ThenInclude(d => d.Executor).FirstOrDefaultAsync();
                    if (end == "false")
                    {
                        if (project != null && project.Name != null && project.StartDate != null && project.EndDate != null && project.Description != null)
                        {
                            if (project.TaskModel.Any())
                            {
                                foreach (var task in project.TaskModel)
                                {
                                    if (task.Name != null && task.Description != null && task.StartDate != null && task.EndDate != null && task.TypeId != null)
                                    {
                                        if (task.SubtasksModel.Any())
                                        {
                                            foreach (var subtask in task.SubtasksModel)
                                            {
                                                if (subtask.Description == null) return false;
                                            }
                                        }
                                    }
                                    else return false;
                                }
                                if (project.TaskModel.Any(d => !d.Executor.Any()))
                                {
                                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                                    foreach (var task in project.TaskModel)
                                    {
                                        var executor = new ExecutorTask
                                        {
                                            id = Guid.NewGuid().ToString(),
                                            user = user,
                                        };
                                        task.Executor.Add(executor);
                                    }
                                }
                                project.Status = 1;
                                await _context.SaveChangesAsync();
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else if (end == "true")
                    {
                        project.Status = 2;
                        await _context.SaveChangesAsync();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error StartProject: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> FinalTask(string TaskId)
        {
            var task = await _context.Task.FindAsync(TaskId);
            if (task != null)
            {
                if (task.Status == 1) task.Status = 2;
                _context.Task.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            return false ;
        }

        public async Task<bool> NoTask(string TaskId)
        {
            var task = await _context.Task.FindAsync(TaskId);
            if (task != null)
            {
                if (task.Status == 2) task.Status = 0;
                _context.Task.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UrgencyTask(string TaskId, int selectedValue)
        {
            try
            {
                var task = await _context.Task.FindAsync(TaskId);
                task.TypeId = selectedValue;
                _context.Task.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error StartProject: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UploadFile(IFormFile file,string TaskId)
        {
            try
            {
                var task = await _context.Task.FindAsync(TaskId);
                if (task == null) return false;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var executor = new Models.File
                    {
                        Name = file.FileName,
                        Expansion = file.ContentType,
                        bytes = memoryStream.ToArray()
                    };
                    task.File = executor;
                }
                _context.Task.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UploadFile: {ex.Message}");
                throw;
            }
        }

        public async Task<Models.File> OpenFile(string taskId)
        {
            var task = await _context.Task.Where(d=>d.Id == taskId).Include(d=>d.File).FirstOrDefaultAsync();

            if (task == null || task.File == null)
            {
                throw new FileNotFoundException("The file was not found.");
            }

            return task.File;
        } 
    }
}
