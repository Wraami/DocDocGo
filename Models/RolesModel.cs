using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class RolesModel
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
