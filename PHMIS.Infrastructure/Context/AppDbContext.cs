using Microsoft.EntityFrameworkCore;
using PHMIS.Domain.Entities;
using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Infrastructure.DatabaseSeeders;

namespace PHMIS.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seed Database
            PatientSeed.DataSeed(modelBuilder);
            LabTestGroupSeed.DataSeed(modelBuilder);
            LabTestSeed.DataSeed(modelBuilder);
            #endregion

            modelBuilder.Entity<LabTest>()
                .HasOne(x => x.LabTestGroup)
                .WithMany()
                .HasForeignKey(x => x.LabTestGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<LabTestGroup> LabTestGroups { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
    }
}
