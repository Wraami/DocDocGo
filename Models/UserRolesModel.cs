using DocDocGo.Model;
using System.ComponentModel.DataAnnotations;

namespace DocDocGo.Models
{
    public class UserRolesModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public UserModel User { get; set; }
        public RolesModel Role { get; set; }
    }
}
