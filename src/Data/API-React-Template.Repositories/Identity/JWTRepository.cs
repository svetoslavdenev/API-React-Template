namespace APIReactTemplate.Repositories.Identity
{
    using Microsoft.EntityFrameworkCore;
    using APIReactTemplate.DataAccess;
    using APIReactTemplate.Domain.Identity;
    using APIReactTemplate.Repositories.Common;
    using System;
    using System.Threading.Tasks;

    public class JWTRepository : ApplicationBaseRepository<JWT>, IJWTRepository<JWT>
    {
        public JWTRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task DeactivateJWT(string value)
        {
            var jwt = await Context.JWT.SingleOrDefaultAsync(t => t.Value == value);
            jwt.IsActive = false;

            await Context.SaveChangesAsync();
        }

        public async Task AddJWT(string value, string userId, DateTime expirationDate)
        {
            var newJwt = new JWT
            {
                UserId = userId,
                AddedOn = DateTime.UtcNow,
                Value = value,
                ExpirationDate = expirationDate,
                IsActive = true
            };

            Context.JWT.Add(newJwt);

            await Context.SaveChangesAsync();
        }

        public async Task<JWT> GetJWTByValue(string value)
        {
            var jwt = await Context.JWT.SingleOrDefaultAsync(t => t.Value == value);

            return jwt;
        }
    }
}
