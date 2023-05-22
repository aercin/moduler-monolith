using core_domain;
using core_domain.Entitites;
using core_infrastructure.persistence;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace core_infrastructure.Services
{
    public static class EventKeeper
    {
        public static async Task StoreEventsAsync(this BaseDbContext context)
        {
            var integrationEvents = context.ChangeTracker.Entries<AggregateRootBase>()
                                                         .Where(x => x.Entity.IntegrationEvents != null && x.Entity.IntegrationEvents.Any())
                                                         .SelectMany(x => x.Entity.IntegrationEvents).ToList();

            var createdOn = DateTime.Now;

            var tasks = integrationEvents.Select(async (integrationEvent) =>
            {
                var integrationEventMessage = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType());

                await context.Set<OutboxMessage>().AddAsync(OutboxMessage.CreateOutboxMessage(integrationEvent.GetType().AssemblyQualifiedName, integrationEventMessage, createdOn));
            });

            await Task.WhenAll(tasks);
        }
    }
}
