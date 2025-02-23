using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StealAllTheCats.API.Swagger;

public class SwaggerJsonRequestBodyOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody != null)
        {
            foreach (var content in operation.RequestBody.Content.Values)
            {
                content.Schema.Type = "object";
            }
        }
    }
}
