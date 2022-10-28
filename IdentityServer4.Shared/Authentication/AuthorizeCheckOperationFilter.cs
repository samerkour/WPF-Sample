using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using IdentityServer4.Shared.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace IdentityServer4.Shared.Authentication
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly AdminApiConfiguration _adminApiConfiguration;

        public AuthorizeCheckOperationFilter(AdminApiConfiguration adminApiConfiguration)
        {
            _adminApiConfiguration = adminApiConfiguration;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);

            //add the Accept-Language header to the Swagger UI interface
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "string" },
                Required = false
            });



            var hasAuthorize = context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                                                            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"}
                            }
                        ] = new[] { _adminApiConfiguration.OidcApiName }
                    }
                };

            }
        }
    }
}
