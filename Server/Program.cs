using System;
using NServiceBus;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Saga Server";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("SSDC.Server");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press any key to stop server...");
                Console.ReadKey();
            }
        }
    }
}