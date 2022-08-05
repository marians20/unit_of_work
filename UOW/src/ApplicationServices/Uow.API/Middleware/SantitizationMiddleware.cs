using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using Uow.Domain.Extensions;

namespace Uow.API.Middleware;

internal class SantitizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SantitizationMiddleware> _logger;

    public SantitizationMiddleware(RequestDelegate next, ILogger<SantitizationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        AdoptClientCulture(context);
        var requestId = context.Request.CreateRequestIdIfNotExists();
        var userEmail = context.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.OrdinalIgnoreCase))?.Value;
        _logger.LogInformation($"{DateTime.Now:G}|{userEmail}|{context.Request.GetDisplayUrl()}");
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