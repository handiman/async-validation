using System;

namespace PoC.Messaging.Events
{
    public sealed class CommandValidationSucceededEvent
    {
        public Guid CommandId { get; set; }
    }
}