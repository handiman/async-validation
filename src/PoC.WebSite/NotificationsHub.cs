using JetBrains.Annotations;
using Microsoft.AspNet.SignalR;
using PoC.Messaging.Events;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace PoC.WebSite
{
    [UsedImplicitly]
    public sealed class NotificationsHub : Hub
        , IHandleMessages<CommandValidationFailedEvent>
        , IHandleMessages<CommandValidationSucceededEvent>
    {
        private static int _instanceCount;

        public NotificationsHub()
        {
            System.Diagnostics.Debug.WriteLine($"Instantiaded {++_instanceCount} times.");
        }

        public override async Task OnConnected()
        {
            await Notify($"Hello from {nameof(NotificationsHub)}!");
        }

        async Task IHandleMessages<CommandValidationFailedEvent>.Handle(CommandValidationFailedEvent domainEvent)
        {
            await Notify("Validation failed");
        }

        async Task IHandleMessages<CommandValidationSucceededEvent>.Handle(CommandValidationSucceededEvent domainEvent)
        {
            await Notify("Validation succeeded");
        }

        private Task Notify(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext(nameof(NotificationsHub));
            context.Clients.All.notify(message);
            return Task.CompletedTask;
        }
    }
}