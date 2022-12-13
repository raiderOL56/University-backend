using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace VersionControl
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // Add Swagger Documentation for each API version
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }
        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            OpenApiInfo openApiInfo = new OpenApiInfo()
            {
                Title = "My .Net API resful",
                Version = description.ApiVersion.ToString(),
                Description = "This is my first API Versioning Control",
                Contact = new OpenApiContact
                {
                    Email = "test@prueba.com",
                    Name = "Martin"
                }
            };

            if (description.IsDeprecated)
            {
                openApiInfo.Description += "This API has been depretcated";
            }

            return openApiInfo;
        }
    }
}