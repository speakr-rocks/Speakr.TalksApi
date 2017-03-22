using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Models.FeedbackForm;
using System.Collections.Generic;

namespace Speakr.TalksApi.Tests.Areas.Feedback
{
    [TestFixture]
    public class WhenCallingPostReview
    {
        private IDapper _db;
        private IRepository _dbRepository;
        private ReviewsController _reviewsController;

        private int _talkId;
        private int _expectedReviewId;
        private string _easyAccessKey;
        private FeedbackResponse _request;

        [SetUp]
        public void Setup()
        {
            _db = A.Fake<IDapper>();
            _dbRepository = new Repository(_db);
            _reviewsController = new ReviewsController(_dbRepository);
            _request = new FeedbackResponse();

            _talkId = 9999;
            _expectedReviewId = 1000;

            _request = new FeedbackResponse
            {
                TalkId = _talkId
            };
        }

        [Test]
        public void ThenItReturns201IfSuccessful()
        {
            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("SELECT Id"),
                    A<object>.Ignored)
                ).Returns(new List<int> { _talkId });

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Reviews`"),
                    A<object>.Ignored)
                ).Returns(new List<int> { _expectedReviewId });

            var result = _reviewsController.PostReviewForTalk(_talkId, _request);
            var response = (CreatedAtActionResult)result;

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            Assert.That(response.StatusCode, Is.EqualTo(201));
            Assert.That(response.ActionName, Is.EqualTo("GetReviewById"));
            Assert.That(response.ControllerName, Is.EqualTo("Reviews"));
            Assert.That(response.Value, Is.EqualTo("Feedback Response Successfully Saved"));
            Assert.That(response.RouteValues["TalkId"], Is.EqualTo(_talkId));
            Assert.That(response.RouteValues["ReviewId"], Is.EqualTo(_expectedReviewId));
        }

        [Test]
        public void Returns409IfTalkIdDontMatch()
        {
            var result = _reviewsController.PostReviewForTalk(12345, _request);
            var response = (ObjectResult)result;

            Assert.That(result, Is.TypeOf<ObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(409));
            Assert.That(response.Value, Is.EqualTo("Talk Id does not match the post body"));

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("SELECT Id"),
                    A<object>.Ignored)
                ).MustNotHaveHappened();

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Reviews`"),
                    A<object>.Ignored)
                ).MustNotHaveHappened();
        }

        [Test]
        public void Returns409IfTalkIdIsInvalid()
        {
            var invalidTalkId = 0;

            var result = _reviewsController.PostReviewForTalk(0, _request);
            var response = (ObjectResult)result;

            Assert.That(result, Is.TypeOf<ObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(409));
            Assert.That(response.Value, Is.EqualTo("Invalid talk Id"));

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("SELECT Id"),
                    A<object>.Ignored)
                ).MustNotHaveHappened();

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Reviews`"),
                    A<object>.Ignored)
                ).MustNotHaveHappened();
        }

        [Test]
        public void Returns409IfTalkDoesNotExist()
        {
            A.CallTo(() => 
                _db.Query<int>(
                    A<string>.That.Contains("SELECT Id"),
                    A<object>.Ignored)
                ).Returns(new List<int> { });

            var result = _reviewsController.PostReviewForTalk(_talkId, _request);
            var response = (ObjectResult)result;

            Assert.That(result, Is.TypeOf<ObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(409));
            Assert.That(response.Value, Is.EqualTo("TalkId could not be found"));

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Reviews`"),
                    A<object>.Ignored)
                ).MustNotHaveHappened();
        }
    }
}
