using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using AVIV.SharedKernel.Interface;
using AVIV.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AVIV.SharedKernel.Interfaces;
using AVIV.Domain.Entities;
using AVIV.Domain.Entities.Advertisement;

namespace AVIV.Infrastructure.Data
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;
        private readonly IDateTime _dateTime;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IDomainEventService domainEventService,
            IDateTime dateTime
            )
        {
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<Advertisement> Advertisements { get; set; }

        Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker IApplicationDbContext.ChangeTracker { get => base.ChangeTracker; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                var dbPath = System.IO.Path.Join(path, "aviv.db");

                optionsBuilder.UseSqlite("Data Source=aviv.db");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? _currentUserService?.UserId;
                        entry.Entity.Created = entry.Entity.Created != DateTimeOffset.MinValue ? entry.Entity.Created : _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService?.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}