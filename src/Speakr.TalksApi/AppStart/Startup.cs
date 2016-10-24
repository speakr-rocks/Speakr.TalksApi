using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Speakr.TalksApi
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            GenerateConfigs(env);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDependencies(services);

            SetupSwagger(services);

            services.AddMvc();
        }
    }
}
