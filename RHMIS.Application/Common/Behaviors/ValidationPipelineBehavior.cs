using FluentValidation;
using MediatR;
using RHMIS.Application.Common.Response;

namespace RHMIS.Application.Common.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!this._validators.Any())
            {
                return await next();
            }

            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    this._validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
                );

                var errors = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(error => error is not null)
                    .Select(error => new ValidationError
                    {
                        Code = error.ErrorCode,
                        Property = error.PropertyName[(error.PropertyName.IndexOf('.') + 1)..].ToLower(),
                        Description = error.ErrorMessage
                    })
                    .Distinct()
                    .ToList();

                var invalidMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result<Unit>.WithErrors), new[] { typeof(List<ValidationError>) });

                if (errors.Any() && invalidMethod != null)
                {
                    return (TResponse)invalidMethod.Invoke(null, new object?[] { errors })!;
                }
            }


            return await next();
        }
    }
}
