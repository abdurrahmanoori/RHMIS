using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Mappings;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Interceptors;
using Scrutor;

namespace PHMIS.Infrastructure.Extensions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddApplicationDbContext();
            services.AddScoped<AuditInterceptor>();

            // Automatically register repositories with Scrutor
            services.Scan(scan => scan
                .FromAssemblies(typeof(InfrastructureServicesRegistration).Assembly)
                    .AddClasses(c => c.InNamespaces(
                        "PHMIS.Infrastructure.Repositories",
                        "PHMIS.Infrastructure.RepositoryStores"))
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
