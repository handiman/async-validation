using System;

namespace PoC.Messaging.Commands
{
    public sealed class DummyCommand
    {
        public Guid Id { get; set; }

        public bool ShouldFail { get; set; }

        public bool LongRunning { get; set; }
    }
}
