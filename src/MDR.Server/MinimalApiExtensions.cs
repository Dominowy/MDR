
using FluentValidation;
using MediatR;

namespace MDR.Server
{
    public static class MinimalApiExtensions
    {
        public static async Task<IResult> Send<TRequest>(this HttpContext httpContext, TRequest request, IMediator mediator, CancellationToken cancellationToken = default)
        {
            var validator = httpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .Select(e => new { e.PropertyName, e.ErrorCode, e.ErrorMessage });

                    return Results.BadRequest(new { Errors = errors });
                }
            }

            var response = await mediator.Send(request, cancellationToken);

            return Results.Ok(response);
        }

        public static async Task<IResult> Validate<TRequest>(this HttpContext httpContext, TRequest request, IMediator mediator, CancellationToken cancellationToken = default)
        where TRequest : class
        {
            var validator = httpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                      .Select(e => new { e.PropertyName, e.ErrorCode, e.ErrorMessage });

                    return Results.BadRequest(new { Errors = errors });
                }
            }

            return Results.Ok();
        }
    }

}
