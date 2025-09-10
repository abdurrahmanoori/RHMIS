using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Repositories.Base;
using PHMIS.Application.Repositories.Patients;
using PHMIS.Application.Repositories.Provinces;
using PHMIS.Application.Repositories.Laboratory;
using PHMIS.Infrastructure.Repositories.Base;
using PHMIS.Infrastructure.Repositories.Patients;
using PHMIS.Infrastructure.Repositories.Provinces;
using PHMIS.Infrastructure.Repositories.Laboratory;
using PHMIS.Infrastructure.RepositoryStores;

namespace PHMIS.Infrastructure.Configurations
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ILabTestGroupRepository, LabTestGroupRepository>();
            services.AddScoped<ILabTestRepository, LabTestRepository>();


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
