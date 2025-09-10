using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PHMIS.Domain.Entities;
using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Domain.Entities.Patients;
using PHMIS.Domain.Enums;
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

            // Store LabStatus as string in PatientLabTest
            modelBuilder.Entity<PatientLabTest>()
                .Property(x => x.LabStatus)
                .HasConversion(new EnumToStringConverter<LabStatus>());
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<LabTestGroup> LabTestGroups { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<PatientLabTest> PatientLabTests { get; set; }
    }
}
