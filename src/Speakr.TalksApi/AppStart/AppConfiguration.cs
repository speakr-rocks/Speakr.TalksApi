using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Speakr.TalksApi.AppStart
{
    public class AppConfiguration
    {
        public static IConfigurationRoot Configure(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("auth0.json");

            var configurationRoot = builder.Build();
            GenerateDbConnectionString(configurationRoot);

            return configurationRoot;
        }

        private static void GenerateDbConnectionString(IConfiguration configuration)
        {
            var dbServer = configuration["SPEAKR_DB_SERVER"];
            var dbName = configuration["SPEAKR_DB_NAME"];
            var dbUserName = configuration["SPEAKR_DB_USER"];
            var dbPassword = configuration["SPEAKR_DB_PASSWORD"];
            const string allowUserVariables = "Allow User Variables=True";

            configuration["DbConnectionString"] =
                $"Server={dbServer};Database={dbName};Uid={dbUserName};Pwd={dbPassword};{allowUserVariables}";
        }
    }
}