using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Controllers.FeedbackForms
{
    [TestFixture]
    public class WhenCallingGetFeedbackForms
    {
        [Test]
        public async Task FeedbackFormsReturns200()
        {
            FeedbackFormsController talkFormsController = new FeedbackFormsController();
            var result = await talkFormsController.GetFeedbackFormsAsync("12345");
            var response = (OkObjectResult)result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task FeedbackFormsReturnsForm()
        {
            FeedbackFormsController talkFormsController = new FeedbackFormsController();
            var result = await talkFormsController.GetFeedbackFormsAsync("12345");
            var response = (OkObjectResult)result;
            var model = (FeedbackForm)response.Value;

            Assert.That(response.Value, Is.TypeOf<FeedbackForm>());
            Assert.That(model.TalkId, Is.EqualTo("12345"));
        }

        [Test]
        public async Task FeedbackFormsReturns404()
        {
            FeedbackFormsController talkFormsController = new FeedbackFormsController();
            var result = await talkFormsController.GetFeedbackFormsAsync("abcde");
            var response = (NotFoundResult)result;

            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.That(response.StatusCode, Is.EqualTo(404));
        }
    }
}
