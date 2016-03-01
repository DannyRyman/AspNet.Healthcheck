using System.Collections.Generic;

namespace AspNetHealthCheck
{
    /// <summary>
    /// Used to configure health check custom steps.
    /// </summary>
    public class HealthCheckConfig
    {
        public HealthCheckConfig()
        {
            Steps = new List<HealthCheckStep>();
        }

        public IList<HealthCheckStep> Steps { get; private set; }

        public void AddStep(HealthCheckStep step)
        {
            Steps.Add(step);
        }
    }
}
