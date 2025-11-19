namespace WindTurbine.Entities
{
    public class GeneratedReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        
        public byte[] Content { get; set; }
    }
}