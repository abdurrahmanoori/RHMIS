using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHMIS.Application.Common.Behaviors;
using PHMIS.Application.Features.Patients.Commands;
using Sigtas.Application.Features.TaxRole.MaintainEnterprise.CommandValidators;
using System.Reflection;

namespace PHMIS.Application.Extentions;

public static class ApplicationServiceRegistration
{


    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssemblyContaining(typeof(CreatePatientCommandValidator),
            includeInternalTypes: true);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }


  
}
