using Microsoft.OpenApi.Models;
using SID.Domain.Base;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SID.Api.Swagger
{
    internal class StronglyTypedIdSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is not null && context?.Type.IsAssignableTo(typeof(StronglyTypedId<>)) == true)
            {
                schema.AdditionalPropertiesAllowed = false;
                schema.Properties.Clear();
            }
        }
    }
}