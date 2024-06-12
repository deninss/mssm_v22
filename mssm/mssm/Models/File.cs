namespace mssm.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Expansion { get; set; }
        public byte[]? bytes { get; set; }
    }
}
