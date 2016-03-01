using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetHealthCheck
{
    public class HealthCheckHandler : HttpMessageHandler
    {
        private readonly HealthCheckConfig _config;
        private readonly string _applicationVersion;

        public HealthCheckHandler(HealthCheckConfig config, string applicationVersion)
        {
            _config = config;
            _applicationVersion = applicationVersion;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stepResponseTasks = _config.Steps.Select(step => Task<HealthCheckStepResponse>.Factory.StartNew(
                state =>
                {
                    var result = step.PerformHealthCheck();

                    return new HealthCheckStepResponse(result.IsSuccessful, step.StepName, step.StepDescription, step.IsCritical, result.Latency, result.DependentStepResponses);
                }
                    ,
                step, cancellationToken));

            var healthCheckStepReponses = await Task.WhenAll(stepResponseTasks);

            var responseBody = new HealthCheckResponse(_applicationVersion, healthCheckStepReponses);

            return request.CreateResponse(responseBody.IsSuccessful ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable, responseBody);
        }

    }
}
