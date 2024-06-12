using System.ComponentModel.DataAnnotations;

namespace mssm.Models.ProjectModel
{
    public class TypeTask
    {
        [Key]
        public int Id { get; set; }
        public string NameType { get; set; }
        public ICollection<TaskModel>? task { get; set; }
        public TypeTask()
        {
            task = new List<TaskModel>();
        }
    }
}
