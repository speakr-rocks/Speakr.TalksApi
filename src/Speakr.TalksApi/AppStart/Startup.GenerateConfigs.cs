using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Speakr.TalksApi
{
    public partial class Startup
    {
        private void GenerateConfigs(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            MakeDbConnectionString();
        }

        private void MakeDbConnectionString()
        {
            var dbServer = Configuration["SPEAKR_DB_SERVER"];
            var dbName = Configuration["SPEAKR_DB_NAME"];
            var dbUserName = Configuration["SPEAKR_DB_USER"];
            var dbPassword = Configuration["SPEAKR_DB_PASSWORD"];

            Configuration["DbConnectionString"] =
                $"Server={dbServer};Database={dbName};Uid={dbUserName};Pwd={dbPassword};";
        }
    }
}
