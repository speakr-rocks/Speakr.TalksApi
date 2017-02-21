using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DataObjects;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Models.Talks;
using Speakr.TalksApi.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Talks
{
    [TestFixture]
    public class WhenCallingPostTalk
    {
        private int _talkId = 12345551;
        private string _easyAccessKey = "clever_einstein";
        private DateTime _expectedStartTime;
        private TalkEntity _expectedTalk;
        private TalkCreationRequest _request;

        private IDapper _db;
        private IRepository _dbRepository;
        private TalksController _talksController;

        [SetUp]
        public void Setup()
        {
            _db = A.Fake<IDapper>();
            _dbRepository = new Repository(_db);
            _expectedStartTime = DateTime.Now.AddDays(7);

            _talksController = new TalksController(_dbRepository);

            _expectedTalk = TalkEntityBuilder.BuildTalkEntityById(_talkId, _easyAccessKey);

            _request = new TalkCreationRequest
            {
                Name = "Test Talk",
                EasyAccessKey = "12345",
                Topic = "Development",
                Description = "This is only a test talk!",
                SpeakerName = "Test Speaker Name",
                TalkStartTime = _expectedStartTime
            };
        }

        [Test]
        public async Task ThenItReturns201IfSuccessful()
        {
            var result = await _talksController.PostTalk(_request);
            var response = (CreatedAtActionResult)result;

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
            Assert.That(response.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public async Task CreatesAQuestionnaireRecordAndReturnsQuestionnaireId()
        {
            var result = await _talksController.PostTalk(_request);
            var response = (CreatedAtActionResult)result;

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Questionnaires`"),
                    A<object>.Ignored)
                ).MustHaveHappened();
        }

        [Test]
        public async Task CreatesATalkRecordWithCorrectQuestionnaireId()
        {
            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Questionnaires`"),
                    A<object>.Ignored)
                ).Returns(new List<int> { 10 });

            var result = await _talksController.PostTalk(_request);
            var response = (CreatedAtActionResult)result;

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Talks`"),
                    A<TalkEntity>.That.Matches(x => x.QuestionnaireId == 10))
                ).MustHaveHappened();
        }

        [Test]
        public async Task ThenItReturnsCreatedResponseProperlyFilledIn()
        {
            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT"),
                    A<object>.Ignored)
                ).Returns(new List<int> { 1 });

            A.CallTo(() =>
                _db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Talks`"),
                    A<TalkEntity>.That.Matches(x => x.QuestionnaireId == 1))
                ).Returns(new List<int> { 111111 } );

            var result = await _talksController.PostTalk(_request);
            var response = (CreatedAtActionResult)result;

            Assert.That(response.ActionName, Is.EqualTo("GetTalkById"));
            Assert.That(response.Value, Is.EqualTo(111111));
        }
    }
}
