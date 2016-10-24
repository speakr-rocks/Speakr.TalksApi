using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Models.Talks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Talks
{
    [TestFixture]
    public class WhenCallingPostTalk
    {
        [Test]
        public async Task ThenItReturns201IfSuccessful()
        {
            var db = A.Fake<IDapper>();

            var dbRepository = new Repository(db);
            var expectedStartTime = DateTime.Now.AddDays(7);

            var request = new TalkCreationRequest
            {
                TalkName = "Test Talk",
                TalkEasyAccessKey = "12345",
                TalkTopic = "Development",
                Description = "This is only a test talk!",
                SpeakerName = "Test Speaker Name",
                TalkStartTime = expectedStartTime
            };

            var talksController = new TalksController(dbRepository);
            var result = await talksController.PostTalk(request);
            var response = (CreatedAtRouteResult)result;

            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
            Assert.That(response.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public async Task CreatesAQuestionnaireIdRecord()
        {
            var db = A.Fake<IDapper>();

            var dbRepository = new Repository(db);
            var expectedStartTime = DateTime.Now.AddDays(7);

            var request = new TalkCreationRequest
            {
                TalkName = "Test Talk",
                TalkEasyAccessKey = "12345",
                TalkTopic = "Development",
                Description = "This is only a test talk!",
                SpeakerName = "Test Speaker Name",
                TalkStartTime = expectedStartTime
            };

            var talksController = new TalksController(dbRepository);
            var result = await talksController.PostTalk(request);
            var response = (CreatedAtRouteResult)result;

            A.CallTo(() =>
                db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Questionnaires`"),
                    A<object>.Ignored)
                ).MustHaveHappened();
        }

        [Test]
        public async Task CreatesATalkWithCorrectQuestionnaireId()
        {
            var db = A.Fake<IDapper>();

            A.CallTo(() =>
                db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Questionnaires`"),
                    A<object>.Ignored)
                ).Returns(new List<int> { 10 });

            var dbRepository = new Repository(db);
            var expectedStartTime = DateTime.Now.AddDays(7);

            var request = new TalkCreationRequest
            {
                TalkName = "Test Talk",
                TalkEasyAccessKey = "12345",
                TalkTopic = "Development",
                Description = "This is only a test talk!",
                SpeakerName = "Test Speaker Name",
                TalkStartTime = expectedStartTime
            };

            var talksController = new TalksController(dbRepository);
            var result = await talksController.PostTalk(request);
            var response = (CreatedAtRouteResult)result;

            A.CallTo(() =>
                db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Talks`"),
                    A<TalkDTO>.That.Matches(x => x.QuestionnaireId == 10))
                ).MustHaveHappened();
        }

        [Test]
        public async Task ThenItReturnsCreatedResponseProperlyFilledIn()
        {
            var db = A.Fake<IDapper>();

            A.CallTo(() =>
                db.Query<int>(
                    A<string>.That.Contains("INSERT"),
                    A<object>.Ignored)
                ).Returns(new List<int> { 1 });

            A.CallTo(() =>
                db.Query<int>(
                    A<string>.That.Contains("INSERT INTO `Talks`"),
                    A<TalkDTO>.That.Matches(x => x.QuestionnaireId == 1))
                ).Returns(new List<int> { 111111 } );

            var dbRepository = new Repository(db);
            var expectedStartTime = DateTime.Now.AddDays(7);

            var request = new TalkCreationRequest
            {
                TalkName = "Test Talk",
                TalkEasyAccessKey = "12345",
                TalkTopic = "Development",
                Description = "This is only a test talk!",
                SpeakerName = "Test Speaker Name",
                TalkStartTime = expectedStartTime
            };

            var talksController = new TalksController(dbRepository);
            var result = await talksController.PostTalk(request);
            var response = (CreatedAtRouteResult)result;

            Assert.That(response.RouteName, Is.EqualTo("GetTalkById"));
            Assert.That(response.Value, Is.EqualTo(111111));
        }
    }
}
