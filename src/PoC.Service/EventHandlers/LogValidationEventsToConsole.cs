﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PoC.Messaging.Events;
using Rebus.Handlers;

namespace PoC.Service.EventHandlers
{
    [UsedImplicitly]
    public sealed class LogValidationEventsToConsole
        : IHandleMessages<CommandValidationFailedEvent>
        , IHandleMessages<CommandValidationSucceededEvent>
    {
        Task IHandleMessages<CommandValidationFailedEvent>.Handle(CommandValidationFailedEvent message)
        {
            Console.WriteLine("Command validation failed");
            return Task.CompletedTask;
        }

        Task IHandleMessages<CommandValidationSucceededEvent>.Handle(CommandValidationSucceededEvent message)
        {
            Console.WriteLine("Command validation succeeded");
            return Task.CompletedTask;
        }
    }
}