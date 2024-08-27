using Common.Response;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Infrastructure.MediatR;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    IServiceProvider serviceProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var validators1 = serviceProvider.GetServices<IValidator<TRequest>>().ToArray();
        if (validators1.Any())
        {
            var failures = validators1
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                var result = Activator.CreateInstance<TResponse>();
                if (result is ServiceResponse response)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = nameof(HttpStatusCode.BadRequest);
                    response.ServiceSubStatus = failures
                        .Select(error => new ServiceSubStatus
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Subject = error.PropertyName,
                            ErrorMessage = error.ErrorMessage
                        }).ToList();
                }
                return result;
            }
        }
        return await next();
    }
}