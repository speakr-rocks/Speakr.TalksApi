using Microsoft.Extensions.DependencyInjection;
using Speakr.TalksApi.Swagger;
using Swashbuckle.Swagger.Model;

namespace Speakr.TalksApi.AppStart
{
    public class SwaggerBootstrap
    {
        public static void SetupSwagger(IServiceCollection services)
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
                options.OperationFilter<ApplySwaggerDescriptionFilterAttributes>();
            });
        }
    }
}