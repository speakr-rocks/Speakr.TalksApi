using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;

namespace Speakr.TalksApi.AppStart
{
    public class IoCRegistry
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider => configuration);
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IDapper, DataAccess.DbAccess.Dapper>();
        }
    }
}