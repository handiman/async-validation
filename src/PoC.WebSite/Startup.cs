using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JetBrains.Annotations;
using Microsoft.Owin;
using Owin;
using PoC.Messaging;
using PoC.Messaging.Commands;
using PoC.Messaging.Events;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;

[assembly: OwinStartup(typeof(PoC.WebSite.Startup))]

namespace PoC.WebSite
{
    [UsedImplicitly]
    public sealed class Startup
    {
        [UsedImplicitly]
        public void Configuration(IAppBuilder app)
        {
            var container = DependencyConfiguration();
            
            var httpConfig = new HttpConfiguration();
            httpConfig.MapHttpAttributeRoutes();
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseWebApi(httpConfig)
               .MapSignalR();
        }

        private static IContainer DependencyConfiguration()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterRebus(cfg =>  cfg
                .Logging(logging => 
                    logging.None())
                .Transport(transport => 
                    transport.UseRabbitMq("amqp://localhost", "subscriber"))
                .Routing(routing => 
                    routing.TypeBased().MapAssemblyOf<DummyCommand>("publisher"))
            );

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterHandlersFromAssemblyOf<Startup>();
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
