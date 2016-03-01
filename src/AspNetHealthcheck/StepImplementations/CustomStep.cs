using System;
using System.Collections.Generic;

namespace AspNetHealthCheck.StepImplementations
{
    public class CustomStep : HealthCheckStep
    {
        private readonly string _stepName;
        private readonly string _stepDescription;
        private readonly bool _isCritical;
        private readonly Func<bool> _checkHealth;

        public CustomStep(
            string stepName,
            string stepDescription,
            bool isCritical,
            Func<bool> checkHealth)
        {
            _stepName = stepName;
            _stepDescription = stepDescription;
            _isCritical = isCritical;
            _checkHealth = checkHealth;
        }

        public override string StepName
        {
            get { return _stepName; }
        }


        public override string StepDescription
        {
            get { return _stepDescription; }
        }


        public override bool IsCritical
        {
            get { return _isCritical; }
        }

        protected override Tuple<bool, IEnumerable<HealthCheckStepResponse>> CheckIsHealthy()
        {
            return new Tuple<bool, IEnumerable<HealthCheckStepResponse>>(_checkHealth.Invoke(), new HealthCheckStepResponse[0]);
        }
    }
}
