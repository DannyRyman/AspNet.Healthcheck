using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AspNetHealthCheck
{
    /// <summary>
    /// Used by HealthCheckResponse. Represents the response to an individual step within an overall healthcheck.
    /// </summary>
    [DataContract]
    public class HealthCheckStepResponse
    {
        public HealthCheckStepResponse(bool isStepSuccessful,
            string stepName,
            string additionalInformation,
            bool isCritical,
            TimeSpan latency,
            IEnumerable<HealthCheckStepResponse> dependentStepResponses)
        {
            IsStepSuccessful = isStepSuccessful;
            StepName = stepName;
            AdditionalInformation = additionalInformation;
            IsCritical = isCritical;
            Latency = latency;

            DependentStepResponses = dependentStepResponses;
        }

        [DataMember]
        public bool IsStepSuccessful { get; private set; }

        [DataMember]
        public string StepName { get; private set; }

        [DataMember]
        public string AdditionalInformation { get; private set; }

        [DataMember]
        public bool IsCritical { get; private set; }

        [DataMember]
        public TimeSpan Latency { get; private set; }

        [DataMember]
        public IEnumerable<HealthCheckStepResponse> DependentStepResponses { get; private set; }
    }
}
