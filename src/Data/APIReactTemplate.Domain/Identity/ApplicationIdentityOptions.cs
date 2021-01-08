namespace APIReactTemplate.Domain.Identity
{
    using Microsoft.AspNetCore.Identity;

    public static class ApplicationIdentityOptions
    {
        public static void GetOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        }
    }
}
