using System;
using NServiceBus;

namespace Shared
{
    public class OrderItem : IMessage
    {
        public Guid OrderId { get; set; }
        public string Item { get; set; }
        public int Price { get; set; }
    }
}