using DocDocGo.Models;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.DAL
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<PrescriptionModel> Prescriptions { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet <UserRolesModel> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRolesModel>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }

    }
}
