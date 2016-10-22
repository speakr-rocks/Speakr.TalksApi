using FakeItEasy;
using NUnit.Framework;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speakr.TalksApi.Tests.DbRepository
{
    [TestFixture]
    public class WhenCallingVerifyConnection
    {
        [Test]
        public void QueryReturns1()
        {
            var dapperDb = A.Fake<IDapper>();
            A.CallTo(() => dapperDb.Query<string>("SELECT '1'")).Returns(new List<string> { "1" });

            var repo = new Repository<string>(dapperDb);
            var result = repo.VerifyConnection();

            Assert.That(result, Is.EqualTo("1"));
        }
    }
}
