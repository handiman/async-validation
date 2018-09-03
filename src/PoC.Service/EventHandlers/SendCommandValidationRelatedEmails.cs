using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PoC.Messaging.Events;
using Rebus.Handlers;

namespace PoC.Service.EventHandlers
{
    [UsedImplicitly]
    public sealed class SendCommandValidationRelatedEmails : IHandleMessages<CommandValidationFailedEvent>
    {
        Task IHandleMessages<CommandValidationFailedEvent>.Handle(CommandValidationFailedEvent domainEvent)
        {
            Console.WriteLine("Faking sending email on validation failure.");
            return Task.CompletedTask;
        }
    }
}
