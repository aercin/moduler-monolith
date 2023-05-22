using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace core_domain
{
    public abstract class AggregateRootBase
    {
        private List<DomainEvent> domainEvents;
        private List<IntegrationEvent> integrationEvents;
        public AggregateRootBase()
        {
            domainEvents = new List<DomainEvent>();
            integrationEvents = new List<IntegrationEvent>();
        }

        public ReadOnlyCollection<DomainEvent> DomainEvents
        {
            get
            {
                return domainEvents.AsReadOnly();
            }
        }

        public ReadOnlyCollection<IntegrationEvent> IntegrationEvents
        {
            get
            {
                return integrationEvents.AsReadOnly();
            }
        }

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public void AddIntegrationEvent(IntegrationEvent integrationEvent)
        {
            integrationEvents.Add(integrationEvent);
        }
    }
}
