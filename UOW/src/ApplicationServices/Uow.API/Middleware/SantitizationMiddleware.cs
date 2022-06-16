using System.Globalization;
using Uow.Domain.Extensions;

namespace Uow.API.Middleware;

internal class SantitizationMiddleware
{
    private readonly RequestDelegate _next;

    public SantitizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        AdoptClientCulture(context);
        context.Request.CreateRequestIdIfNotExists();

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }

    private static void AdoptClientCulture(HttpContext context)
    {
        var cultureQuery = context.Request.Query["culture"];
        if (string.IsNullOrWhiteSpace(cultureQuery))
        {
            return;
        }

        var culture = new CultureInfo(cultureQuery);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }
}