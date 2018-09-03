using System;

namespace PoC.Messaging.Events
{
    public sealed class CommandValidationFailedEvent
    {
        public Guid CommandId { get; set; }
    }
}