using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocDocGo.Models
{
    public class ReportModel
    {
        [Key]
        public int ReportId { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public string ReportType { get; set;}
        public bool IsReportPrinted { get; set; }

        public PatientModel Patient { get; set; }
    }
}
