using System;
using NServiceBus;

namespace Shared
{
    public class SubmitOrder : IMessage
    {
        public Guid OrderId { get; set; }
    }
}