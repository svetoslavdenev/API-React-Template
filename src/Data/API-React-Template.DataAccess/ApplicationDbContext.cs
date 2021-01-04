namespace APIReactTemplate.DataAccess
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using APIReactTemplate.Domain;
    using APIReactTemplate.Domain.Common.Interfaces;
    using APIReactTemplate.Domain.Identity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        public DbSet<DummyModel> Dummy { get; set; }

        public DbSet<JWT> JWT { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.SetTimeTrackingRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.SetTimeTrackingRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetTimeTrackingRules()
        {
            var changedEntries = this.ChangeTracker.Entries().Where(x =>
            x.Entity is ITimeTrackable &&
            (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var item in changedEntries)
            {
                var entity = item.Entity as ITimeTrackable;
                if (item.State == EntityState.Added && entity.AddedOn == default)
                {
                    var now = DateTime.UtcNow;
                    entity.AddedOn = now;
                    entity.LastModifiedOn = now;
                }
            }
        }
    }
}
