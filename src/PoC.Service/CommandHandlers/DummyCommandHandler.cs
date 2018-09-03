using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PoC.Messaging.Commands;
using PoC.Messaging.Events;
using Rebus.Bus;
using Rebus.Handlers;

namespace PoC.Service.CommandHandlers
{
    [UsedImplicitly]
    public sealed class DummyCommandHandler : IHandleMessages<DummyCommand>
    {
        private readonly IBus _bus;

        public DummyCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        async Task IHandleMessages<DummyCommand>.Handle(DummyCommand command)
        {
            if (command.LongRunning)
            {
                Console.WriteLine("Faking long running validation. Sleeping for 30 seconds.");
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
            else
            {
                Console.WriteLine("Faking validation");
            }

            if (command.ShouldFail)
            {
                await _bus.Publish(new CommandValidationFailedEvent
                {
                    CommandId = command.Id
                });
            }
            else
            {
                await _bus.Publish(new CommandValidationSucceededEvent
                {
                    CommandId = command.Id
                });
            }
        }
    }
}