using Microsoft.EntityFrameworkCore;
using mssm.Context;
using mssm.Models.ProjectModel;
using System.Security.Claims;

namespace mssm.Services.ITask
{
    public class _Task : _ITask
    {
        public ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _Task(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TaskModel>> GetAllTask()
        {
            try
            {
                var tasks = await _context.Task
                    .Where(t => t.Project.Status == 1 && t.Status != 2 &&
                        t.Executor.Any(e => e.user.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                    .Include(d=>d.File)
                    .ToListAsync();

                if (tasks != null)
                {
                    var type = tasks.GroupBy(d => d.TypeId).OrderBy(d => d.Key);

                    var sortedTasks = new List<TaskModel>();

                    foreach (var item in type)
                    {
                        var sortedSubset = item
                            .Select(task => new
                            {
                                Task = task,
                                Rez = (task.EndDate.Value - DateTime.Now.Date)
                            })
                            .OrderByDescending(x => x.Rez)
                            .Select(x => x.Task)
                            .ToList();

                        sortedTasks.AddRange(sortedSubset);
                    }

                    Console.WriteLine($"Output of tasks: " + DateTime.Now);
                    return sortedTasks;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetAllProject: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> EndTask(string TaskID)
        {
            try
            {
                var task = await _context.Task.FindAsync(TaskID);
                if(task != null)
                {
                    if(task.Status == 0) task.Status = 1;
                    _context.Task.Update(task);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error EndTask: {ex.Message}");
                throw;
            }
           
        }

        public async Task<bool> UploadFile(IFormFile file, string TaskId)
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
            var task = await _context.Task.Where(d => d.Id == taskId).Include(d => d.File).FirstOrDefaultAsync();

            if (task == null || task.File == null)
            {
                throw new FileNotFoundException("The file was not found.");
            }

            return task.File;
        }
    }
}
