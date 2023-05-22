using System;

namespace core_domain
{
    public class InboxMessage
    {
        public int Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string MessageId { get; private set; }
        public string ConsumerType { get; private set; }

        private InboxMessage() { }

        public static InboxMessage CreateInboxMessage(string type, string messageId, DateTime createdOn)
        {
            return new InboxMessage
            {
                ConsumerType = type,
                MessageId = messageId,
                CreatedOn = createdOn
            };
        }
    }
}
