using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Mappings;
using PHMIS.Infrastructure.Configurations;
using PHMIS.Infrastructure.Context;
using PHMIS.Infrastructure.Interceptors;

namespace PHMIS.Infrastructure.Extentions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddApplicationDbContext();
            services.AddScoped<AuditInterceptor>();
            // services.AddDbContext<SigtasDbContext>(
            //    options => options.UseOracle(configuration.GetConnectionString("SigtasDbContext")),
            //    ServiceLifetime.Transient);
            //services.AddDbContext<AppDbContext>(
            //    options => options.UseSqlite(

            //        // configuration.GetConnectionString("SigtasDbContext"),
            //        new SqliteConnection(configuration.GetConnectionString("sqliteConnection")),
            //        oracleOptions =>
            //        {
            //            // oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
            //        })
            //    // .WithExpressionExpanding()
            //    );

            //services.AddSingleton<SigtasDapperDbContext>();


            //// services.AddDbContext<SigtasDbContext>(
            ////    optionx => optionx.UseOracle(configuration.GetConnectionString("SigtasDbContext"))
            ////    , ServiceLifetime.Transient);

            //// services.AddDbContext<SigtasDbContext>(
            ////    options => options.UseOracle(
            ////        //configuration.GetConnectionString("SigtasDbContext"),
            ////        new OracleConnection(configuration.GetConnectionString("SigtasDbContext")),

            //// oracleOptions =>
            ////        {
            ////            oracleOptions.UseOracleSQLCompatibility("19");
            ////        }),
            ////    ServiceLifetime.Transient);
            //services.AddAutoMapper(options =>
            //{
            //    // options.AddCollectionMappers();
            //    // options.UseEntityFrameworkCoreModel<SigtasDbContext>(services);
            //}, typeof(AdministrationProfile).Assembly);

            services.AddRepositories();
            services.AddAutoMapper((serviceProvider, cfg) =>
            {
                cfg.AddCollectionMappers();
                cfg.UseEntityFrameworkCoreModel<AppDbContext>(serviceProvider);
            },
              typeof(MappingProfile).Assembly);

            // services.AddScoped<SigtasDbContext>();
            return services;
        }

    }
}
