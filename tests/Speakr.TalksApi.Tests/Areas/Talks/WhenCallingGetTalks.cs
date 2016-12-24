using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DataObjects;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Talks
{
    [TestFixture]
    public class WhenCallingGetTalks
    {
        private TalkEntity _expectedTalk1;
        private TalkEntity _expectedTalk2;
        private TalkEntity _expectedTalk3;
        private TalkEntity _expectedTalk4;
        private List<TalkEntity> _expectedResponse;

        private IDapper _db;
        private IRepository _dbRepository;
        private TalksController _talksController;

        [SetUp]
        public void Setup()
        {
            _db = A.Fake<IDapper>();
            _dbRepository = new Repository(_db);
            _talksController = new TalksController(_dbRepository);

            _expectedTalk1 = TalkEntityBuilder.BuildTalkEntityById(1, "key_1");
            _expectedTalk2 = TalkEntityBuilder.BuildTalkEntityById(2, "key_2");
            _expectedTalk3 = TalkEntityBuilder.BuildTalkEntityById(3, "key_3");
            _expectedTalk4 = TalkEntityBuilder.BuildTalkEntityById(4, "key_4");

            _expectedResponse = new List<TalkEntity> { _expectedTalk1, _expectedTalk2, _expectedTalk3, _expectedTalk4 };
        }

        [Test]
        public async Task ThenItReturns200IfSuccessful()
        {
            var result = await _talksController.GetTalks();
            var response = (OkObjectResult)result;
            var model = (List<TalkEntity>)response.Value;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task WhenNoFiltersAreSuppliedThenItReturnsAListOfAllTalks()
        {
            A.CallTo(() =>
                _db.Query<TalkEntity>(
                    A<string>.That.Contains("SELECT * FROM `Talks`"),
                    null)
                ).Returns(_expectedResponse);

            var result = await _talksController.GetTalks();
            var response = (OkObjectResult)result;
            var model = (List<TalkEntity>)response.Value;

            Assert.That(model[0].Id, Is.EqualTo(1));
            Assert.That(model[1].Id, Is.EqualTo(2));
            Assert.That(model[2].Id, Is.EqualTo(3));
            Assert.That(model[3].Id, Is.EqualTo(4));
        }

        [Test]
        public async Task WhenBySpeakerFilterIsAppliedOnlyRightTalksAreReturned()
        {

        }

        //Other filters as necessary
    }
}
