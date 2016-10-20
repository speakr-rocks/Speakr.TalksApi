using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Speakr.TalksApi.DataAccess;

namespace Speakr.TalksApi.Tests.DataAccess
{
    [TestFixture]
    public class WhenVerifyingDbConnection
    {
        [Test]
        public void VerifyConnectionDoesNotThrowAndReturns1()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection();

            var config = builder.Build();
            config["DbConnectionString"] = "Server=127.0.0.1;Database=speakrdb;Uid=root;Pwd=root;";

            var dapperDb = new Speakr.TalksApi.DataAccess.Dapper.Dapper(config);

            var repo = new Repository<int>(dapperDb);
            var result = repo.VerifyConnection();

            Assert.That(result, Is.EqualTo("1"));
        }
    }
}
