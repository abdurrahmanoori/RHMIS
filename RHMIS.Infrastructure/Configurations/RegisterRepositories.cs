using Microsoft.Extensions.DependencyInjection;
using RHMIS.Application.Repositories.Base;
using RHMIS.Infrastructure.Repositories.Base;
using RHMIS.Infrastructure.RepositoryStores;

namespace RHMIS.Infrastructure.Configurations
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddAdministrationRepositories();
            //services.AddTaxRollRepositories();
            //services.AddTaxAccountRepositories();
            //services.AddAssessmentRepositories();
            //services.AddCashingRepositories();
            //services.AddReportRepositories();
            //services.AddDocFileRepositories();
            //services.AddAuditRepositories();
            //services.AddEnforcementRepositories();

            services.AddScoped<RepositoriesStore>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
