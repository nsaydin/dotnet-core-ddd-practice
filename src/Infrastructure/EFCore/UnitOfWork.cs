using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Domain.Entity;
using Core.Event;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext, IDomainEventPublisher domainEventPublisher)
        {
            _dbContext = dbContext;
            _domainEventPublisher = domainEventPublisher;
        }

        public async Task SaveChangesAsync()
        {
            var entities = GetDomainEventEntities(_dbContext.ChangeTracker);
            await _dbContext.SaveChangesAsync();
            await PublishEvents(entities);
        }

        private static IEnumerable<IAggregateRoot> GetDomainEventEntities(ChangeTracker changeTracker)
        {
            return changeTracker.Entries<IAggregateRoot>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToList();
        }

        private async Task PublishEvents(IEnumerable<IAggregateRoot> entities)
        {
            foreach (var entity in entities)
            {
                await _domainEventPublisher.PublishEvents(entity.DomainEvents);

                entity.ClearEvents();
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}