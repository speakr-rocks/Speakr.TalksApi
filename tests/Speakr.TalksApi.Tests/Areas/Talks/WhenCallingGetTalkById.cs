using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Models.Talks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Talks
{
    [TestFixture]
    public class WhenCallingGetTalkById
    {
        [Test]
        public async Task Returns200IfSuccessful()
        {
            var talkId = 12345551;

            var expectedDto = new TalkDTO
            {
                Id = talkId,
                TalkEasyAccessKey = "Clever_Einstein",
                TalkName = "Talk 101",
            };

            var db = A.Fake<IDapper>();

            A.CallTo(() =>
                db.Query<TalkDTO>(
                    A<string>.That.Contains("SELECT"),
                    A<object>.Ignored)
                ).Returns(new List<TalkDTO> { expectedDto });

            var dbRepository = new Repository(db);
            var talksController = new TalksController(dbRepository);
            var result = await talksController.GetTalkById(talkId);
            var response = (OkObjectResult)result;
            var model = (TalkDTO)response.Value;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(model.Id, Is.EqualTo(talkId));
        }
    }
}
