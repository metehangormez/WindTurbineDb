using System;

namespace WindTurbine.DTOs.Reports
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        public string Title { get; set; }      
        public DateTime GeneratedDate { get; set; }
        public string FileType { get; set; }    
        public string FileSize { get; set; }    
        public string Status { get; set; }     
    }
}