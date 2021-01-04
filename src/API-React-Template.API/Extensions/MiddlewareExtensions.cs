namespace APIReactTemplate.API.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using APIReactTemplate.API.Middlewares;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTVerification(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VerifyJWTMiddleware>();
        }
    }
}
