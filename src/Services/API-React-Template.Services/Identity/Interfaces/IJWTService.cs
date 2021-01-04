namespace APIReactTemplate.Services.Identity.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IJWTService
    {
        Task<bool> IsValidToken(string value);

        Task AddJWT(string value, string userId, DateTime expirationDate);

        Task DeactivateJWT(string value);
    }
}
