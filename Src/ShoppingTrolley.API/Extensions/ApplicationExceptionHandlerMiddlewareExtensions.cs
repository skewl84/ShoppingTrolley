using Microsoft.AspNetCore.Builder;
using ShoppingTrolley.API.Middleware;

namespace ShoppingTrolley.API.Extensions
{
    public static class ApplicationExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationExceptionHandlerMiddleware>();
        }
    }
}
