using Common.Utilities;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Api.Configuration
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            Assert.NotNull(services, nameof(services));

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopManagerMaui", Version = "v1" });
                // add xml comments
                c.IncludeXmlComments(xmlFilename);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
                c.CustomSchemaIds(type => type.FullName);
            });

        }
        public static IApplicationBuilder UseSwaggerAndUI(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            //Swagger middleware for generate "Open API Documentation" in swagger.json
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
