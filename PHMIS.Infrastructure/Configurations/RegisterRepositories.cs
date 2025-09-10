using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Repositories.Base;
using PHMIS.Infrastructure.Repositories.Base;
using PHMIS.Infrastructure.RepositoryStores;

namespace PHMIS.Infrastructure.Configurations
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Scrutor handles assembly scanning; retain generic base and UoW if directly referenced here
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<RepositoriesStore>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
