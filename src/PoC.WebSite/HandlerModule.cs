using Autofac;
using Rebus.Config;

namespace PoC.WebSite
{
    internal sealed class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterHandlersFromAssemblyOf<HandlerModule>();
        }
    }
}