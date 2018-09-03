using System.Threading.Tasks;
using System.Web.Http;
using JetBrains.Annotations;
using PoC.Messaging.Commands;
using Rebus.Bus;

namespace PoC.WebSite
{
    [UsedImplicitly]
    public sealed class JobsController : ApiController
    {
        private readonly IBus _bus;

        public JobsController(IBus bus)
        {
            _bus = bus;
        }

        [Route("api/jobs"), HttpPost]
        public async Task Post(CreateJobCommand command)
        {
            await _bus.Send(command);
        }
    }
}