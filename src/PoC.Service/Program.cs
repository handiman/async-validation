using System;
using Autofac;
using PoC.Messaging;
using PoC.Messaging.Commands;
using PoC.Messaging.Events;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace PoC.Service
{
    internal sealed class Program
    {
        private static void Main()
        {
            Console.WriteLine("Press enter to exit...");
            using (DependencyConfiguration())
            {
                Console.ReadLine();
            }
        }

        private static IDisposable DependencyConfiguration()
        {
            var builder = new ContainerBuilder();

            builder.RegisterRebus(cfg => cfg
                .Logging(logging =>
                    logging.None())
                .Transport(transport => 
                    transport.UseRabbitMq("amqp://localhost", "publisher"))
            );

            builder.RegisterHandlersFromAssemblyOf<Program>();
            builder.RegisterBuildCallback(container =>
            {
                var bus = container.Resolve<IBus>();
                bus.Subscribe<CommandValidationFailedEvent>();
                bus.Subscribe<CommandValidationSucceededEvent>();
            });

            return builder.Build();
        }
    }
}
