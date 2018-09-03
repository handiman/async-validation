using System.Linq;
using Autofac;
using PoC.Messaging.Commands;
using PoC.Messaging.Events;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace PoC.Messaging
{
    public sealed class RebusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRebus(ConfigureRebus);
            builder.RegisterBuildCallback(SubscribeToDomainEvents);
        }

        private static RebusConfigurer ConfigureRebus(RebusConfigurer cfg)
        {
            const string queueName = "queue";
            return cfg
                .Logging(logging => logging
                    .None())
                .Transport(transport => transport
                    .UseRabbitMq("amqp://localhost", queueName))
                .Routing(routing => routing
                    .TypeBased()
                    .MapAssemblyOf<DummyCommand>(queueName));
        }

        private static void SubscribeToDomainEvents(IComponentContext container)
        {
            var domainEvents = typeof(CommandValidationSucceededEvent).Assembly.GetTypes().Where(x => x.Namespace == typeof(CommandValidationSucceededEvent).Namespace);
            var bus = container.Resolve<IBus>();
            foreach (var domainEvent in domainEvents)
            {
                bus.Subscribe(domainEvent);
            }
        }
    }
}
 