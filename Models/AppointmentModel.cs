using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class AppointmentModel
    {
        [Key]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string? Topic { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Status { get; set; }  
        public string? Notes { get; set; }
    }
}
