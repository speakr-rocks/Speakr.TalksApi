using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using System;

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

    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerDescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public SwaggerDescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }

    public class ApplySwaggerDescriptionAttributeFilterAttributes : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var attributes = context.ApiDescription.GetActionAttributes();
            foreach (var attr in attributes)
            {
                if (attr is SwaggerDescriptionAttribute)
                {
                    operation.Description = ((SwaggerDescriptionAttribute)attr).Description;
                }
            }
        }
    }
}
