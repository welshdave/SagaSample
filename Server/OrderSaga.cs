using System;
using NServiceBus;
using NServiceBus.Saga;
using Shared;

namespace Server
{
    public class OrderSaga : Saga<OrderSagaData>,
        IAmStartedByMessages<OrderItem>,
        IHandleMessages<SubmitOrder>,
        IHandleTimeouts<AbandonOrder>
    {
        public void Handle(OrderItem message)
        {
            Data.OrderId = message.OrderId;
            Data.Items.Add(message.Item);
            Console.Out.WriteLine("{0} added to order {1}", message.Item, message.OrderId);
            RequestTimeout<AbandonOrder>(TimeSpan.FromSeconds(10));
            
        }

        public void Handle(SubmitOrder message)
        {
            //var rand = new Random();
            //if (rand.Next(0, 2) == 0)
            //{
            //    throw new Exception("Problem with message");
            //}
            Console.WriteLine("Order {0} submitted with {1} item(s)", message.OrderId, Data.Items.Count);
            MarkAsComplete();
        }

        public void Timeout(AbandonOrder state)
        {
            Console.WriteLine("Order {0} cancelled",Data.OrderId);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderItem>(message => message.OrderId)
                .ToSaga(sd => sd.OrderId);
            mapper.ConfigureMapping<SubmitOrder>(message => message.OrderId)
                .ToSaga(sd => sd.OrderId);
        }
    }
}