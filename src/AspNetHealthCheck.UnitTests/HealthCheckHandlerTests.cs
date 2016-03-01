using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AspNetHealthCheck.StepImplementations;
using AspNetHealthCheck.UnitTests.Support;
using NUnit.Framework;

namespace AspNetHealthCheck.UnitTests
{
    [TestFixture]
    public class HealthCheckHandlerTests
    {
        [Test]
        public async Task ShouldReturnServiceUnavailableWhenStepFails()
        {
            var config = new HealthCheckConfig();
            config.AddStep(new CustomStep("Step1", "Step1 Description", true, () => true));
            config.AddStep(new CustomStep("Step2", "Step2 Description", true, () => false));
            var sut = new HealthCheckHandlerTss(config);
            var response = await sut.CallSendAsync(ContextUtil.CreateHttpRequestMessage(), new CancellationToken(false));
            var responseBody = await response.Content.ReadAsAsync<HealthCheckResponse>();

            Assert.AreEqual(HttpStatusCode.ServiceUnavailable, response.StatusCode);
            Assert.IsFalse(responseBody.IsSuccessful);
            Assert.AreEqual(2, responseBody.Steps.Count);
            Assert.IsTrue(responseBody.Steps[0].IsStepSuccessful);
            Assert.AreEqual("Step1", responseBody.Steps[0].StepName);
            Assert.AreEqual("Step1 Description", responseBody.Steps[0].AdditionalInformation);
            Assert.IsFalse(responseBody.Steps[1].IsStepSuccessful);
            Assert.AreEqual("Step2", responseBody.Steps[1].StepName);
            Assert.AreEqual("Step2 Description", responseBody.Steps[1].AdditionalInformation);
        }

        [Test]
        public async Task ShouldReturnOkWhenNoStepsAreRegistered()
        {
            var config = new HealthCheckConfig();
            var sut = new HealthCheckHandlerTss(config);
            var response = await sut.CallSendAsync(ContextUtil.CreateHttpRequestMessage(), new CancellationToken(false));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task ShouldReturnOkWhenAllStepsAreSuccessful()
        {
            var config = new HealthCheckConfig();
            config.AddStep(new CustomStep("Step1", "Step1 Description", true, () => true));
            config.AddStep(new CustomStep("Step2", "Step2 Description", true, () => true));
            var sut = new HealthCheckHandlerTss(config);
            var response = await sut.CallSendAsync(ContextUtil.CreateHttpRequestMessage(), new CancellationToken(false));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
