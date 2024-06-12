using mssm.Context;

namespace mssm.Models.ProjectModel
{
    public class ExecutorTask
    {
        public string id { get; set; }
        public UserContext user { get; set; }
        public TaskModel Task { get; set; }

    }
}
