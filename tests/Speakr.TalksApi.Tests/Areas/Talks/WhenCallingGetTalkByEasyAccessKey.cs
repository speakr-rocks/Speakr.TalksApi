using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Speakr.TalksApi.Controllers;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.Areas.Talks
{
    [TestFixture]
    public class WhenCallingGetTalkByEasyAccessKey
    {
        [Test]
        public async Task Returns200IfSuccessful()
        {
            var easyAccessKey = "1234";
            var db = A.Fake<IDapper>();
            var dbRepository = new Repository(db);
            var talksController = new TalksController(dbRepository);

            var result = await talksController.GetTalkByEasyAccessKey(easyAccessKey);
            var response = (OkResult)result;
            
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(response.StatusCode, Is.EqualTo(200));
        }
    }
}
