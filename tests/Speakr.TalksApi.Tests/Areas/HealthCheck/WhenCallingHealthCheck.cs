using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using System.Net;

namespace Speakr.TalksApi.Tests.Areas
{
    [TestFixture]
    public class WhenCallingHealthCheck
    {
        [Test]
        public void ThenHealthCheckReturns200Ok()
        {
            var controller = new HealthCheckController();
            var result = (OkResult)controller.HealthCheck();

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
