using System;
using NServiceBus;
using Shared;

namespace Client
{
    internal class Program
    {
        private static Guid _currentOrderId;

        private static void Main(string[] args)
        {
            Console.Title = "Saga Client";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("SSDC.Client");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Start(bus);
            }
        }

        private static void Start(IBus bus)
        {
            Console.WriteLine("Press '1' to set Order Id");
            Console.WriteLine("Press '2' to Add item to Order");
            Console.WriteLine("Press '3' to Submit Order");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        _currentOrderId = Guid.NewGuid();
                        Console.WriteLine($"Started Order {_currentOrderId}");
                        continue;
                    case ConsoleKey.D2:
                        bus.Send("SSDC.Server", new OrderItem
                        {
                            OrderId = _currentOrderId,
                            Item = $"Item {Guid.NewGuid()}",
                            Price = 1
                        });
                        Console.WriteLine("Added Item to Order {0}", _currentOrderId);
                        continue;
                    case ConsoleKey.D3:
                        bus.Send("SSDC.Server", new SubmitOrder
                        {
                            OrderId = _currentOrderId
                        });
                        Console.WriteLine("Submitted Order {0}", _currentOrderId);
                        continue;
                    default:
                        return;
                }
            }
        }
    }
}