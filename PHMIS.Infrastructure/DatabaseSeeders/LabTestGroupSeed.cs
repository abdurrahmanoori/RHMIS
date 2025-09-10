using Microsoft.EntityFrameworkCore;
using PHMIS.Domain.Entities.Laboratory;

namespace PHMIS.Infrastructure.DatabaseSeeders
{
    public static class LabTestGroupSeed
    {
        public static void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabTestGroup>().HasData(
                new LabTestGroup { Id = 1, Name = "Chemistry", Description = "Chemistry tests", SortOrder = 1 },
                new LabTestGroup { Id = 2, Name = "Hematology", Description = "Blood tests", SortOrder = 2 },
                new LabTestGroup { Id = 3, Name = "Microbiology", Description = "Microbiology tests", SortOrder = 3 }
            );
        }
    }
}
