using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class PrescriptionModel
    {
        [Key]
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public string? Notes { get; set; }   

    }
}
