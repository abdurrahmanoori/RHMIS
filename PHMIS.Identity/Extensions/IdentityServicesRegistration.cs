using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Identity.IServices;
using PHMIS.Domain.Entities.Identity.Entity;
using PHMIS.Identity.Services;

namespace PHMIS.Identity.Extensions
{
    public static class IdentityServicesRegistration
    {
        // Generic registration to use the application's DbContext without creating a separate one here
        public static IServiceCollection ConfigureIdentityServices<TContext>(this IServiceCollection services, IConfiguration? configuration = null)
            where TContext : DbContext
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<TContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }
    }
}
