namespace APIReactTemplate.Repositories.Common
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using APIReactTemplate.DataAccess;
    using APIReactTemplate.Repositories.Common.Interfaces;

    public class ApplicationBaseRepository<TEntity> : IApplicationBaseRepository<TEntity>
        where TEntity : class
    {
        public ApplicationBaseRepository(ApplicationDbContext applicationDbContext)
        {
            this.Context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public IQueryable<TEntity> GetAll() => this.DbSet;

        public IQueryable<TEntity> GetAllAsNoTracking() => this.DbSet.AsNoTracking();

        public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
