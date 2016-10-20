using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger.Model;

namespace Speakr.TalksApi
{
    public partial class Startup
    {
        private void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Speakr.TalksApi",
                    Description = "API for the Speakr.App",
                    TermsOfService = "None"
                });
            });
        }
    }
}
