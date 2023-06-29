using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class PatientModel
    {
        [Key]
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        [EmailAddress]
        public string? EmailAddress { get; set; }
        public short IsPrivatePatient { get; set; }
    }
}
