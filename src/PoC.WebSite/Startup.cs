using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JetBrains.Annotations;
using Microsoft.Owin;
using Owin;
using PoC.Messaging;
using Rebus.Config;

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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<RebusModule>();
            builder.RegisterHandlersFromAssemblyOf<Startup>();
            return builder.Build();
        }
    }
}
