namespace APIReactTemplate.Services.Identity
{
    using System;
    using System.Threading.Tasks;

    using APIReactTemplate.Domain.Identity;
    using APIReactTemplate.Repositories.Identity;
    using APIReactTemplate.Services.Identity.Interfaces;

    public class JWTService : IJWTService
    {
        private readonly IJWTRepository<JWT> jwtRepository;

        public JWTService(IJWTRepository<JWT> jwtRepository)
        {
            this.jwtRepository = jwtRepository;
        }

        public async Task AddJWT(string value, string userId, DateTime expirationDate)
        {
            await this.jwtRepository.AddJWT(value, userId, expirationDate);
        }

        public async Task DeactivateJWT(string value)
        {
            await this.jwtRepository.DeactivateJWT(value);
        }

        public async Task<bool> IsValidToken(string value)
        {
            var jwt = await this.jwtRepository.GetJWTByValue(value);

            if (jwt.IsActive)
            {
                var now = DateTime.UtcNow;

                if (jwt.ExpirationDate < now)
                {
                    await this.jwtRepository.DeactivateJWT(jwt.Value);

                    return false;
                }
            }

            return jwt.IsActive;
        }
    }
}
