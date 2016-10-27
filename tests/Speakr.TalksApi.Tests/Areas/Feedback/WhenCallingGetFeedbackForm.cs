using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Models.FeedbackForm;
using Speakr.TalksApi.Models.Talks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Feedback
{
    [TestFixture]
    public class WhenCallingGetFeedbackForms
    {
        private static int _talkId = 9999999;
        private static string _easyAccessKey = "12345";
        private static TalkDTO _talkDTO;
        private static FeedbackForm _feedbackForm = FeedbackFormStub.GetTalkById(_easyAccessKey);

        private IDapper _dapper;
        private IRepository _dbRepository;
        private FeedbackFormsController _feedbackFormController;

        [SetUp]
        public void SetupTests()
        {
            _dapper = A.Fake<IDapper>();
            _dbRepository = new Repository(_dapper);
            _feedbackFormController = new FeedbackFormsController(_dbRepository);

            _talkDTO = new TalkDTO
            {
                TalkID = _talkId,
                TalkEasyAccessKey = "Clever_Einstein",
                TalkName = "Talk 101",
            };
        }

        [Test]
        public async Task FeedbackFormsReturns200()
        {
            A.CallTo(() =>
                _dapper.Query<TalkDTO>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored)
                ).Returns(new List<TalkDTO> { _talkDTO });

            A.CallTo(() =>
                _dapper.Query<FeedbackForm>(
                    A<string>.That.Contains("SELECT * FROM `questionnaires`"),
                    A<object>.Ignored)
                ).Returns(new List<FeedbackForm> { _feedbackForm });

            var result = await _feedbackFormController.GetFeedbackFormsAsync(_easyAccessKey);
            var response = (OkObjectResult)result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task FeedbackFormsReturnsForm()
        {
            A.CallTo(() =>
                _dapper.Query<TalkDTO>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored)
                ).Returns(new List<TalkDTO> { _talkDTO });

            A.CallTo(() =>
                _dapper.Query<FeedbackForm>(
                    A<string>.That.Contains("SELECT"),
                    A<object>.Ignored)
                ).Returns(new List<FeedbackForm> { _feedbackForm });

            var result = await _feedbackFormController.GetFeedbackFormsAsync("12345");
            var response = (OkObjectResult)result;
            var model = (FeedbackForm)response.Value;

            Assert.That(response.Value, Is.TypeOf<FeedbackForm>());
            Assert.That(model.TalkId, Is.EqualTo("12345"));
            Assert.That(model.Questionnaire.First().QuestionText, Is.EqualTo("How much did you enjoy the talk?"));
        }

        [Test]
        public async Task FeedbackFormsReturns404()
        {
            var expectedForm = FeedbackFormStub.GetTalkById("12345");

            A.CallTo(() =>
                _dapper.Query<FeedbackForm>(
                    A<string>.That.Contains("SELECT"),
                    A<object>.Ignored)
                ).Returns(new List<FeedbackForm> { expectedForm });

            var result = await _feedbackFormController.GetFeedbackFormsAsync("abcde");
            var response = (NotFoundResult)result;

            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.That(response.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task IfTalkNotFoundReturn404()
        {
            A.CallTo(() =>
                _dapper.Query<TalkDTO>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored)
                ).Returns(new List<TalkDTO>());

            var result = await _feedbackFormController.GetFeedbackFormsAsync("talk not found");
            var response = (NotFoundResult)result;

            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.That(response.StatusCode, Is.EqualTo(404));
        }
    }
}
