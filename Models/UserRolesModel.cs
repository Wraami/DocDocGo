using DocDocGo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocDocGo.Models
{
    public class UserRolesModel
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public UserModel User { get; set; }
        public RolesModel Role { get; set; }
    }
}
