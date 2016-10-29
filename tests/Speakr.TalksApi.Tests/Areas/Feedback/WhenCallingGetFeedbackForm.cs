using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Models.FeedbackForm;
using Speakr.TalksApi.Models.Talks;
using Speakr.TalksApi.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Feedback
{
    [TestFixture]
    public class WhenCallingGetFeedbackForms
    {
        private IDapper _db;
        private IRepository _dbRepository;
        private FeedbackFormsController _feedbackFormsController;

        private TalkEntity _expectedTalkDTO;
        private FeedbackForm _expectedFeedbackForm;
        private string _expectedQuestionnaire;
        private int _expectedTalkId = 1111;
        private string _expectedEasyAccessKey = "ThisKeyExist";

        [SetUp]
        public void Setup()
        {
            _db = A.Fake<IDapper>();
            _dbRepository = new Repository(_db);
            _feedbackFormsController = new FeedbackFormsController(_dbRepository);

            _expectedFeedbackForm = FeedbackFormStub.GetTalkById(_expectedTalkId, _expectedEasyAccessKey);
            _expectedTalkDTO = TalkEntityStub.GetTalk(_expectedTalkId, _expectedEasyAccessKey);
            _expectedQuestionnaire = FeedbackFormStub.GetQuestionnaireAsJson();
        }

        [Test]
        public async Task ReturnsCorrect200WhenFormExists()
        {
            A.CallTo(() => _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored))
                .Returns(new List<TalkEntity>() { _expectedTalkDTO });

            A.CallTo(() => _db.Query<string>(
                    A<string>.That.Contains("SELECT `Questionnaire`"),
                    A<object>.Ignored))
                .Returns(new List<string>() { _expectedQuestionnaire });

            var action = await _feedbackFormsController.GetFeedbackFormForTalk("ThisKeyExist");
            var result = (OkObjectResult)action;

            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.TypeOf<FeedbackForm>());
        }

        [Test]
        public async Task Returns404WhenTalkDoesNotExist()
        {
            A.CallTo(() => _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored))
                .Returns(new List<TalkEntity>());

            var action = await _feedbackFormsController.GetFeedbackFormForTalk("KeyDoesNotExist");
            var result = (NotFoundObjectResult)action;

            Assert.That(result.StatusCode, Is.EqualTo(404));
            Assert.That(result.Value, Contains.Substring("KeyDoesNotExist"));
        }

        [Test]
        public async Task Returns404WhenFormDoesNotExist()
        {
            A.CallTo(() => _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored))
                .Returns(new List<TalkEntity>() { _expectedTalkDTO });

            A.CallTo(() => _db.Query<string>(
                    A<string>.That.Contains("SELECT `Questionnaire`"),
                    A<object>.Ignored))
                .Returns(new List<string>() { "" });

            var action = await _feedbackFormsController.GetFeedbackFormForTalk("FormDoesNotExist");
            var result = (NotFoundObjectResult)action;

            Assert.That(result.StatusCode, Is.EqualTo(404));
            Assert.That(result.Value, Contains.Substring($"feedback form with talk id key: {_expectedTalkId}"));
        }

        [Test]
        public async Task ReturnsCorrectlyMappedForm()
        {
            A.CallTo(() => _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT * FROM `talks`"),
                    A<object>.Ignored))
                .Returns(new List<TalkEntity>() { _expectedTalkDTO });

            A.CallTo(() => _db.Query<string>(
                    A<string>.That.Contains("SELECT `Questionnaire`"),
                    A<object>.Ignored))
                .Returns(new List<string>() { _expectedQuestionnaire });

            var action = await _feedbackFormsController.GetFeedbackFormForTalk("ThisKeyExistForm");
            var result = (OkObjectResult)action;
            var feedbackForm = (FeedbackForm)result.Value;

            Assert.That(feedbackForm.TalkId, Is.EqualTo(_expectedTalkId));
            Assert.That(feedbackForm.EasyAccessKey, Is.EqualTo(_expectedEasyAccessKey));
            Assert.That(feedbackForm.TalkName, Is.EqualTo("TalkName"));
            Assert.That(feedbackForm.SpeakerName, Is.EqualTo("SpeakerName"));
            Assert.That(feedbackForm.Questionnaire.Count, Is.EqualTo(6));
            Assert.That(feedbackForm.Description, Is.EqualTo("Description"));
        }
    }
}
