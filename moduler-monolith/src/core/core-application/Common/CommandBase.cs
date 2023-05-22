using MediatR;
using System;
using System.Text.Json.Serialization;

namespace core_application.Common
{
    public abstract class CommandBase<T> : IRequest<T> where T : class
    {
        [JsonIgnore]
        public Guid MessageId { get; set; }
        [JsonIgnore]
        public string ConsumerType { get; set; }
    }
}
