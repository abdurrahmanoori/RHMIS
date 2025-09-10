using Microsoft.EntityFrameworkCore;
using PHMIS.Domain.Entities.Laboratory;

namespace PHMIS.Infrastructure.DatabaseSeeders
{
    public static class LabTestSeed
    {
        public static void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabTest>().HasData(
                new LabTest
                {
                    Id = 1,
                    Name = "Glucose",
                    Description = "Fasting blood glucose",
                    Price = 10,
                    IsActive = true,
                    LabTestGroupId = 1, // Chemistry
                    UnitOfMeasurment = "mg/dL",
                    NormalRange = "70-99",
                    Abbreviation = "FBS"
                },
                new LabTest
                {
                    Id = 2,
                    Name = "Lipid Profile",
                    Description = "Total Cholesterol, HDL, LDL, Triglycerides",
                    Price = 25,
                    IsActive = true,
                    LabTestGroupId = 1, // Chemistry
                    UnitOfMeasurment = "mg/dL",
                    NormalRange = "Varies",
                    Abbreviation = "LP"
                },
                new LabTest
                {
                    Id = 3,
                    Name = "CBC",
                    Description = "Complete Blood Count",
                    Price = 20,
                    IsActive = true,
                    LabTestGroupId = 2, // Hematology
                    UnitOfMeasurment = null,
                    NormalRange = "Varies",
                    Abbreviation = "CBC"
                },
                new LabTest
                {
                    Id = 4,
                    Name = "Urine Culture",
                    Description = "Microbiology culture",
                    Price = 30,
                    IsActive = true,
                    LabTestGroupId = 3, // Microbiology
                    UnitOfMeasurment = null,
                    NormalRange = "Negative",
                    Abbreviation = "UC"
                }
            );
        }
    }
}
