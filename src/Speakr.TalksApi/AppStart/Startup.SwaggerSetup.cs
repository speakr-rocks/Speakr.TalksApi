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
                options.OperationFilter<ApplySwaggerDescriptionFilterAttributes>();
            });
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerNotesAttribute : Attribute
    {
        public string Description { get; private set; }

        public SwaggerNotesAttribute(string description)
        {
            this.Description = description;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerSummaryAttribute : Attribute
    {
        public string Summary { get; private set; }

        public SwaggerSummaryAttribute(string summary)
        {
            this.Summary = summary;
        }
    }

    public class ApplySwaggerDescriptionFilterAttributes : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var attributes = context.ApiDescription.GetActionAttributes();

            foreach (var attr in attributes)
            {
                if (attr is SwaggerNotesAttribute)
                {
                    operation.Description = ((SwaggerNotesAttribute)attr).Description;
                }

                if (attr is SwaggerSummaryAttribute)
                {
                    operation.Summary = ((SwaggerSummaryAttribute)attr).Summary;
                }
            }
        }
    }
}
