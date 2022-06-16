using System.Reflection;

namespace Uow.API.Extensions;

internal static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
}