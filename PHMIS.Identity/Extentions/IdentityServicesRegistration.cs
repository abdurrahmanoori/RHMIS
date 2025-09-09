using Microsoft.Extensions.DependencyInjection;
using PHMIS.Identity.IServices;
using PHMIS.Identity.Services;

namespace PHMIS.Identity.Extentions
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services)
        {

          services.AddScoped<ICurrentUser, CurrentUser>();
            //services.AddHttpContextAccessor();
            // services.AddScoped<SigtasDbContext>();
            return services;
        }

    }
}
