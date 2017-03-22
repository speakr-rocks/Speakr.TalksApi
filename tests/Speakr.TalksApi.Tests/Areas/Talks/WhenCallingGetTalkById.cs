using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DataObjects;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Tests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Talks
{
    [TestFixture]
    public class WhenCallingGetTalkById
    {
        private int _talkId = 12345551;
        private string _easyAccessKey = "clever_einstein";
        private TalkEntity _expectedTalk;

        private IDapper _db;
        private IRepository _dbRepository;
        private TalksController _talksController;

        [SetUp]
        public void Setup()
        {
            _db = A.Fake<IDapper>();
            _dbRepository = new Repository(_db);
            _talksController = new TalksController(_dbRepository);

            _expectedTalk = TalkEntityBuilder.BuildTalkEntityById(_talkId, _easyAccessKey);
        }

        [Test]
        public async Task ThenItReturns200IfSuccessful()
        {
            A.CallTo(() =>
                _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT"),
                    A<object>.Ignored)
                ).Returns(new List<TalkEntity> { _expectedTalk });

            var result = await _talksController.GetTalkById(_talkId);
            var response = (OkObjectResult)result;
            var model = (TalkEntity)response.Value;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(model.Id, Is.EqualTo(_talkId));
        }

        [Test]
        public async Task ThenItReturns404IfNotFound()
        {
            A.CallTo(() =>
                _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT"),
                    A<object>.Ignored)
                ).Returns(new List<TalkEntity>());

            var result = await _talksController.GetTalkById(_talkId);
            var response = (NotFoundResult)result;

            Assert.That(response, Is.TypeOf<NotFoundResult>());
            Assert.That(response.StatusCode, Is.EqualTo(404));
        }
    }
}
