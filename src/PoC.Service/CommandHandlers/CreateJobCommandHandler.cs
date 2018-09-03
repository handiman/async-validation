using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PoC.Messaging.Commands;
using PoC.Messaging.Events;
using Rebus.Bus;
using Rebus.Handlers;

namespace PoC.Service.CommandHandlers
{
    [UsedImplicitly]
    public sealed class CreateJobCommandHandler : IHandleMessages<CreateJobCommand>
    {
        private readonly IBus _bus;

        public CreateJobCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        async Task IHandleMessages<CreateJobCommand>.Handle(CreateJobCommand message)
        {
            Console.WriteLine("Faking validation.");

            await _bus.Publish(new CommandValidationSucceededEvent
            {
                CommandId = message.Id
            });
        }
    }
}