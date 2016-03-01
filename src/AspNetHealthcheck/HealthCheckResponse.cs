using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AspNetHealthCheck
{
    [DataContract]
    public class HealthCheckResponse
    {
        public HealthCheckResponse(string serviceVersion, IList<HealthCheckStepResponse> steps)
        {
            ServiceVersion = serviceVersion;
            Steps = steps;
            IsSuccessful = Steps.All(x => x.IsStepSuccessful);
        }

        [DataMember]
        public bool IsSuccessful { get; set; }

        [DataMember]
        public string ServiceVersion { get; private set; }

        /// <summary>
        /// The responses for the individual steps
        /// </summary>
        [DataMember]
        public IList<HealthCheckStepResponse> Steps { get; private set; }
    }
}
