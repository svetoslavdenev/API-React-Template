namespace APIReactTemplate.API.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using APIReactTemplate.Services.Identity.Interfaces;
    using System.Threading.Tasks;

    public class VerifyJWTMiddleware
    {
        private readonly RequestDelegate next;

        public VerifyJWTMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJWTService service)
        {
            var jwt = GetJWT(context);

            if (!string.IsNullOrEmpty(jwt))
            {
                var isValid = await service.IsValidToken(jwt);

                if (isValid)
                {
                    await this.next(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                }

            }
            else
            {
                await this.next(context);
            }
        }

        private static string GetJWT(HttpContext context) 
        {
            var jwt = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(jwt))
            {
                jwt = jwt.Split()[1];
            }

            return jwt;
        }
    }
}
