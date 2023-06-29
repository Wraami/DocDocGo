using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocDocGo.Models
{
    public class PrescriptionModel
    {
        [Key]
        public int PrescriptionId { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public bool PaymentNeeded { get; set; }
        public string? Notes { get; set; }

        public PatientModel Patient { get; set; }

    }
}
