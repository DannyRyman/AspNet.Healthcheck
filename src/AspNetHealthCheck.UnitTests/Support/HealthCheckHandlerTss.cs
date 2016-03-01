using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetHealthCheck.UnitTests.Support
{
    public class HealthCheckHandlerTss : HealthCheckHandler
    {
        public HealthCheckHandlerTss(HealthCheckConfig config) : base(config, null)
        {
        }

        public async Task<HttpResponseMessage> CallSendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return await SendAsync(request, cancellationToken);
        }
    }
}
