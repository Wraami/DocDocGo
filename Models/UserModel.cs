using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? ContactNumber { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string NetInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
