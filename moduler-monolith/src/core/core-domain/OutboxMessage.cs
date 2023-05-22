using System;

namespace core_domain.Entitites
{
    public class OutboxMessage
    {
        public int Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string Message { get; private set; } 
        public string Type { get; private set; }

        private OutboxMessage() { }

        public static OutboxMessage CreateOutboxMessage(string type, string message, DateTime createdOn)
        {
            return new OutboxMessage
            {
                Type = type,
                Message = message,
                CreatedOn = createdOn
            };
        }
    }
}
