using PHMIS.Infrastructure.Context;

namespace PHMIS.Test.TestInfrastructure
{
    public async Task CleanupDatabaseAsync( )
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Clear specific tables to avoid FK issues
        db.Patients.RemoveRange(db.Patients);
        await db.SaveChangesAsync();
    }
}
