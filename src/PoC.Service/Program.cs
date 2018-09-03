using System;
using Autofac;
using PoC.Messaging;
using PoC.Service.CommandHandlers;
using Rebus.Config;

namespace PoC.Service
{
    internal static class Program
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
            builder.RegisterModule<RebusModule>();
            builder.RegisterHandlersFromAssemblyOf<CreateJobCommandHandler>();
            return builder.Build();
        }
    }
}
