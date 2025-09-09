using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RHMIS.Infrastructure.Configurations;
using RHMIS.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHMIS.Infrastructure.Extentions
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<SigtasDbContext>(
            //    options => options.UseOracle(configuration.GetConnectionString("SigtasDbContext")),
            //    ServiceLifetime.Transient);
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlite(

                    // configuration.GetConnectionString("SigtasDbContext"),
                    new SqliteConnection(configuration.GetConnectionString("sqliteConnection")),
                    oracleOptions =>
                    {
                        // oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
                    })
                // .WithExpressionExpanding()
                );

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

            // services.AddScoped<SigtasDbContext>();
            return services;
        }

    }
}
