using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class ReportModel
    {
        [Key]
        public int ReportId { get; set; }
        public int PatientId { get; set; }
        public string ReportType { get; set;}
        public bool IsReportPrinted { get; set; }
    }
}
