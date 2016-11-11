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
            _easyAccessKey = "sad_einstein";

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
                    A<string>.That.Contains("INSERT INTO `Feedback`"),
                    A<object>.Ignored)
                ).Returns(new List<int> { _expectedReviewId });

            var result = _reviewsController.PostReviewForTalk("12345", _request);
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
        public void Returns409IfTalkDoesNotExist()
        {

        }
    }
}
