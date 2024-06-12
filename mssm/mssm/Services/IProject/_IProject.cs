using mssm.Models.ProjectModel;
using mssm.Models.TeamModel;

namespace mssm.Services.IProject
{
    public interface _IProject
    { 
        Task<string> CreateProject();
        Task<string> CreateTask(string projectId);
        Task<string> CreateSubtask(string TaskId);
        Task<IEnumerable<ProjectModel>> GetAllProject();
        Task UpdateProject(ProjectModel project);
        Task UpdateTask(TaskModel project);
        Task<bool> UpdateSubtask(SubtasksModel project);
        Task DeleteProject(string projectId);
        Task DeleteTask(string taskId);
        Task DeleteSubtasks(string subtasksId);
        Task<ProjectModel> GetSettingProject(string projectId);
        Task<IEnumerable<Team>> GetSettingProjectTeam();
        Task<List<DepartmentModel>> SaveExecutorDepartment(string ProjectID, string[] Executor, string[] UncheckedDepartments);
        Task<bool> SaveExecutor(string ExecutorId, string TaskId, bool Delete);
        Task<bool> StartProject(string projectId,string end);
        Task<bool> FinalTask(string TaskId);
        Task<bool> NoTask(string TaskId);
        Task<bool> UrgencyTask(string TaskId,int selectedValue);
    
        Task<bool> UploadFile(IFormFile file,string TaskId);
        Task<Models.File> OpenFile(string taskId);
    }
}
