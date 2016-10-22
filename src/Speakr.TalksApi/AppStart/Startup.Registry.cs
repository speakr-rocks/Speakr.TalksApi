using Microsoft.Extensions.DependencyInjection;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;

namespace Speakr.TalksApi
{
    public partial class Startup
    {
        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDapper, DataAccess.DbAccess.Dapper>();
        }
    }
}
