using System;

namespace PoC.Messaging.Commands
{
    public sealed class DummyCommand
    {
        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get => _id;
            set => _id = Guid.Empty == value ? Guid.NewGuid() : value;
        }

        public bool ShouldFail { get; set; }

        public bool LongRunning { get; set; }
    }
}
