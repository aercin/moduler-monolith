using System;

namespace core_domain
{
    public class DomainEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}
