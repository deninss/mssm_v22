namespace mssm.Models.TeamModel
{
    public class AddDepartment
    {
        public string TeamId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Participant> Participants { get; set; }
    }
}
