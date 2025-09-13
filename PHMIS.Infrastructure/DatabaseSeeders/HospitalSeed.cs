using Microsoft.EntityFrameworkCore;
using PHMIS.Domain.Entities;

namespace PHMIS.Infrastructure.DatabaseSeeders
{
    public static class HospitalSeed
    {
        public static void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>().HasData(
                new Hospital
                {
                    Id = 1,
                    Name = "Central Hospital",
                    Code = "H-CENTRAL",
                    Phone = "+1-555-0001",
                    Email = "central@example.com",
                    Address = "100 Main St",
                    City = "Springfield",
                    Country = "US",
                    IsActive = true,
                    PublicId = "a4ec3c8d-2b05-4a3d-9b08-8c6f0d1b4a11"
                },
                new Hospital
                {
                    Id = 2,
                    Name = "Westside Clinic",
                    Code = "H-WEST",
                    Phone = "+1-555-0002",
                    Email = "west@example.com",
                    Address = "200 West Ave",
                    City = "Springfield",
                    Country = "US",
                    IsActive = true,
                    PublicId = "b8b2a2b3-6f02-4c8e-9b47-84a118e8f2c2"
                },
                new Hospital
                {
                    Id = 3,
                    Name = "East Medical Center",
                    Code = "H-EAST",
                    Phone = "+1-555-0003",
                    Email = "east@example.com",
                    Address = "300 East Blvd",
                    City = "Springfield",
                    Country = "US",
                    IsActive = true,
                    PublicId = "c3f4d6e7-8a90-4b2c-b3d4-e5f6a7b8c9d0"
                }
            );
        }
    }
}
