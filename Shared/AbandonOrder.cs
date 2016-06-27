using System;
using NServiceBus;

namespace Shared
{
    public class AbandonOrder : IMessage
    {
        public Guid OrderId { get; set; }
    }
}