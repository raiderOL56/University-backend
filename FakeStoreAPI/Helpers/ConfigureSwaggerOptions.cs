using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace FakeStoreAPI.Helpers
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
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            OpenApiInfo openApiInfo = new OpenApiInfo()
            {
                Title = "Bienvenido a FakeStoreAPI",
                Version = description.ApiVersion.ToString(),
                Description = "Esta API fue desarrollada para consumir los servicios de FakeStoreAPI",
                Contact = new OpenApiContact()
                {
                    Email = "martin@prueba.com",
                    Name = "Martin Carrera Vázquez"
                }
            };

            if (description.IsDeprecated)
            {
                openApiInfo.Description += "This API has been deprecated";
            }

            return openApiInfo;
        }
    }
}