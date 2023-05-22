using System;

namespace core_domain
{
    public class IntegrationEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}
