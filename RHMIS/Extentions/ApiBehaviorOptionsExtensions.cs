using Microsoft.AspNetCore.Mvc;

namespace PHMIS.Extensions
{

    public static class ApiBehaviorOptionsExtensions
    {
        public static IServiceCollection AddCustomModelStateValidation(this IServiceCollection services)
        {
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var validationErrors = context.ModelState
            //            .Where(ms => ms.Value?.Errors.Count > 0)
            //            .SelectMany(kvp => kvp.Value!.Errors.Select(err => new ValidationError
            //            {
            //                Code = "ValidationError",
            //                Property = kvp.Key,
            //                Description = err.ErrorMessage
            //            }))
            //            .ToList();

            //        return new BadRequestObjectResult(validationErrors);
            //    };
            //});

            return services;
        }
    }
}
