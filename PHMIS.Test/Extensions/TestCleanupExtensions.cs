using PHMIS.Infrastructure.Context;
using PHMIS.Test.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace PHMIS.Test.Extensions
{
    public static class TestCleanupExtensions
    {
        public static async Task CleanupPatientsAsync(this CustomWebApplicationFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Patients.RemoveRange(dbContext.Patients);
            await dbContext.SaveChangesAsync();
        }

        public static async Task CleanupLabTestGroupsAsync(this CustomWebApplicationFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.LabTestGroups.RemoveRange(dbContext.LabTestGroups);
            await dbContext.SaveChangesAsync();
        }
    }
}
