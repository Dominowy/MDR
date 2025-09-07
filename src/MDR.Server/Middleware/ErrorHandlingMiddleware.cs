using MDR.Application.Exceptions;

namespace MDR.Server.Middleware
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badHttpRequestException)
            {
                logger.LogError(badHttpRequestException, badHttpRequestException.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badHttpRequestException.Message);
            }
            catch (UnauthorizedException unauthorizedException)
            {
                logger.LogError(unauthorizedException, unauthorizedException.Message);

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(unauthorizedException.Message);
            }
            catch (ForbiddenException forbiddenException)
            {
                logger.LogError(forbiddenException, forbiddenException.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
