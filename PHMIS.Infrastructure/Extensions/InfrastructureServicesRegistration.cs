using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Mappings;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Interceptors;
using Scrutor;

// Plan (pseudocode):
// - Add internal marker types in the target namespaces to avoid magic strings.
// - Use typeof(NamespaceMarker).Namespace to get the namespace string.
// - Use Scrutor .InNamespaces(...) instead of .InNamespacesOf.
// - Keep the rest of the registrations unchanged.

namespace PHMIS.Infrastructure.Repositories
{
    internal sealed class NamespaceMarker { }
}

namespace PHMIS.Infrastructure.RepositoryStores
{
    internal sealed class NamespaceMarker { }
}

namespace PHMIS.Infrastructure.Extensions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDbContext();
            services.AddScoped<AuditInterceptor>();

            // Automatically register repositories with Scrutor (no magic strings)
            services.Scan(scan => scan
                .FromAssemblies(typeof(InfrastructureServicesRegistration).Assembly)
                    .AddClasses(c => c.InNamespaces(
                        typeof(PHMIS.Infrastructure.Repositories.NamespaceMarker).Namespace!,
                        typeof(PHMIS.Infrastructure.RepositoryStores.NamespaceMarker).Namespace!))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            // Preserve explicit registrations for generic base and UoW
            services.AddScoped(typeof(PHMIS.Application.Repositories.Base.IGenericRepository<>), typeof(PHMIS.Infrastructure.Repositories.Base.GenericRepository<>));
            services.AddScoped<PHMIS.Infrastructure.RepositoryStores.RepositoriesStore>();
            services.AddScoped<PHMIS.Application.Repositories.Base.IUnitOfWork, PHMIS.Infrastructure.Repositories.Base.UnitOfWork>();

            services.AddAutoMapper((serviceProvider, cfg) =>
            {
                cfg.AddCollectionMappers();
                cfg.UseEntityFrameworkCoreModel<AppDbContext>(serviceProvider);
            },
              typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
