using Microsoft.EntityFrameworkCore;
using PHMIS.Domain.Entities.Patients;

namespace PHMIS.Infrastructure.DatabaseSeeders
{
    public static class PatientSeed
    {
        public static void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Gender = "Male",
                    PhoneNumber = "555-1234",
                    Email = "john.doe@example.com",
                    Address = "123 Main St, Springfield"
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1990, 8, 22),
                    Gender = "Female",
                    PhoneNumber = "555-5678",
                    Email = "jane.smith@example.com",
                    Address = "456 Elm St, Springfield"
                },
                new Patient
                {
                    Id = 3,
                    FirstName = "Alex",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1978, 12, 3),
                    Gender = "Other",
                    PhoneNumber = "555-9012",
                    Email = "alex.johnson@example.com",
                    Address = "789 Oak St, Springfield"
                }
            );
        }
    }
}
