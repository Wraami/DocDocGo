using DocDocGo.Model;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

    }
}
