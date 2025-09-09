using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RHMIS.Application.Common.Behaviors;
using System.Reflection;

namespace RHMIS.Application.Extentions;

public static class ApplicationServiceRegistration
{


    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        //services.AddValidatorsFromAssemblyContaining(typeof(RejectDublicateCommandValidator),
        //    includeInternalTypes: true);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }


  
}
