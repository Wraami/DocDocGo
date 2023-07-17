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
        [Required]
        public string ReportType { get; set;}
        public bool IsReportPrinted { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ReportCreationTime { get; set; }
        public string ReportStatus { get; set; }
        public PatientModel Patient { get; set; }

        public string StaffName { get; set; }
    }
}
