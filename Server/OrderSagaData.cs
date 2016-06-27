using System;
using System.Collections.Generic;
using NServiceBus.Saga;

namespace Server
{
    public class OrderSagaData : IContainSagaData
    {
        public OrderSagaData()
        {
            Items = new List<string>();
        }

        public Guid OrderId { get; set; }
        public List<string> Items { get; set; }
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
    }
}