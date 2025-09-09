using Microsoft.OpenApi.Models;

namespace RHMIS.Extentions
{
    public static class SwaggerServiceCollectionExtensions
    {
        /*
         PSEUDOCODE PLAN
         - Create static extension class SwaggerServiceCollectionExtensions in same namespace used in Program (Khayati.Api.Extentions).
         - Implement extension method AddSwaggerWithJwt(IServiceCollection services).
           - Inside call services.AddSwaggerGen with same configuration currently inline in Program.cs:
             - Define Swagger doc "v1" with title "Khayati API".
             - Add security definition "Bearer" (ApiKey in header named Authorization, scheme Bearer, BearerFormat JWT, description with example).
             - Add security requirement referencing that scheme (empty scopes).
           - Return services for fluent chaining.
         - No external dependencies beyond Microsoft.OpenApi.Models.
         - This allows Program.cs to call builder.Services.AddSwaggerWithJwt() instead of duplicating code.
        */
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Khayati API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...`"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     System.Array.Empty<string>()
                 }
             });
            });

            return services;
        }
    }
}
