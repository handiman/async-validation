using System;

namespace PoC.Messaging.Commands
{
    public sealed class CreateJobCommand
    {
        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get => _id;
            set => _id = Guid.Empty == value ? Guid.NewGuid() : value;
        }
    }
}
