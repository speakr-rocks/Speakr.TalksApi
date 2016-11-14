using System;

namespace Speakr.TalksApi.Swagger
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerSummaryAttribute : Attribute
    {
        public string Summary { get; private set; }

        public SwaggerSummaryAttribute(string summary)
        {
            this.Summary = summary;
        }
    }
}