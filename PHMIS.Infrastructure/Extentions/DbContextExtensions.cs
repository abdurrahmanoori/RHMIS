using Microsoft.Extensions.DependencyInjection;
using PHMIS.Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PHMIS.Infrastructure.Interceptors;

namespace PHMIS.Infrastructure.Extentions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                // Current working directory (e.g., Khayati.Api)
                var current = Directory.GetCurrentDirectory();

                // Get parent directory (remove last segment: Khayati.Api)
                string parentDirectory = Path.GetDirectoryName(current)!;

                // Path to database inside Infrastructure/Databases
                var dbPath = Path.Combine(parentDirectory, "PHMIS.Infrastructure", "Databases", "rhmisDb.db");

                // Ensure directory exists
                var dbDirectory = Path.GetDirectoryName(dbPath)!;
                if (!Directory.Exists(dbDirectory))
                {
                    Directory.CreateDirectory(dbDirectory);
                }

                //var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                var interceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

                options.UseSqlite($"Data Source={dbPath}",
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                       .AddInterceptors(interceptor);

                options.EnableSensitiveDataLogging(true);
            });

            return services;
        }
    }
}
