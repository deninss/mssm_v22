using mssm.Models.ProjectModel;

namespace mssm.Services.ITask
{
    public interface _ITask
    {
        Task<IEnumerable<TaskModel>> GetAllTask();
        Task<bool> EndTask(string TaskID);
        Task<bool> UploadFile(IFormFile file, string TaskId);
        Task<Models.File> OpenFile(string taskId);
    }
}
