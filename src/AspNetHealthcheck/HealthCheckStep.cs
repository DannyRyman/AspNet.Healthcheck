using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AspNetHealthCheck
{
    /// <summary>
    /// A custom health check step
    /// </summary>
    public abstract class HealthCheckStep
    {
        /// <summary>
        /// The name of the step.
        /// </summary>
        public abstract string StepName { get; }

        /// <summary>
        /// The description of the step.
        /// </summary>
        public abstract string StepDescription { get; }

        /// <summary>
        /// Whether the step is critical
        /// </summary>
        public abstract bool IsCritical { get; }

        /// <summary>
        /// Specify whether the step is healthy
        /// </summary>
        /// <returns></returns>
        public HealthCheckStepResult PerformHealthCheck()
        {
            var sw = new Stopwatch();

            sw.Start();
            var result = CheckIsHealthy();
            sw.Stop();

            return new HealthCheckStepResult(result.Item1, sw.Elapsed, result.Item2);
        }

        protected abstract Tuple<bool, IEnumerable<HealthCheckStepResponse>> CheckIsHealthy();
    }
}