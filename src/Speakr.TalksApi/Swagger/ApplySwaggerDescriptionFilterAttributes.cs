using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;

namespace Speakr.TalksApi.Swagger
{
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