using System;
using System.Collections.Generic;

namespace AspNetHealthCheck
{
    public struct HealthCheckStepResult
    {
        public HealthCheckStepResult(bool isSuccessful, TimeSpan latency, IEnumerable<HealthCheckStepResponse> dependentStepResponses)
            : this()
        {
            IsSuccessful = isSuccessful;
            Latency = latency;
            DependentStepResponses = dependentStepResponses;
        }

        /// <summary>
        /// Whether the step is successful
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// How long the step took to return a result.
        /// </summary>
        public TimeSpan Latency { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<HealthCheckStepResponse> DependentStepResponses { get; private set; }
    }
}