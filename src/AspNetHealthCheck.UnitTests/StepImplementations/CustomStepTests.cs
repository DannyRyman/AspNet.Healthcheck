using AspNetHealthCheck.StepImplementations;
using NUnit.Framework;

namespace AspNetHealthCheck.UnitTests.StepImplementations
{
    [TestFixture]
    public class CustomStepTests
    {
        [Test]
        public void ShouldSetTheExpectedParametersBasedOnWhatIsPassedIntoTheConstructor()
        {
            var step = new CustomStep("StepName", "StepDescription", false, () => false);

            Assert.AreEqual("StepName", step.StepName);
            Assert.AreEqual("StepDescription", step.StepDescription);
            Assert.AreEqual(false, step.IsCritical);
        }

        [Test]
        public void ShouldReturnSuccessfulWhenCustomStepFuncReturnedTrue()
        {
            var step = new CustomStep("StepName", "StepDescription", false, () => true);
            var result = step.PerformHealthCheck();
            Assert.IsTrue(result.IsSuccessful);
        }

        [Test]
        public void ShouldReturnUnsuccessfulWhenCustomStepFuncReturnedFalse()
        {
            var step = new CustomStep("StepName", "StepDescription", false, () => false);
            var result = step.PerformHealthCheck();
            Assert.IsFalse(result.IsSuccessful);
        }
    }
}
