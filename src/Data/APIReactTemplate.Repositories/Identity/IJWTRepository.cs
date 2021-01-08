namespace APIReactTemplate.Repositories.Identity
{
    using System;
    using System.Threading.Tasks;

    using APIReactTemplate.Domain.Identity;
    using APIReactTemplate.Repositories.Common.Interfaces;

    public interface IJWTRepository<TEntity> : IApplicationBaseRepository<TEntity>
        where TEntity : JWT
    {
        Task<JWT> GetJWTByValue(string value);

        Task AddJWT(string value, string userId, DateTime expirationDate);

        Task DeactivateJWT(string value);
    }
}
