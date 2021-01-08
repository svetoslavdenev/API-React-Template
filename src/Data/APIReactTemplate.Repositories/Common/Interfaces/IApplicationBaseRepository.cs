namespace APIReactTemplate.Repositories.Common.Interfaces
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IApplicationBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
