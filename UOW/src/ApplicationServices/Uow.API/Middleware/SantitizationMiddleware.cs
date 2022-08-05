using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using Uow.Domain;
using Uow.Domain.Extensions;

namespace Uow.API.Middleware;

internal class SantitizationMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<SantitizationMiddleware> logger;

    public SantitizationMiddleware(RequestDelegate next, ILogger<SantitizationMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        AdoptClientCulture(context);
        var requestId = context.Request.CreateRequestIdIfNotExists();
        var userEmail = context.User.Claims
            .FirstOrDefault(c => c.Type.Contains(Constants.Claims.Email, StringComparison.OrdinalIgnoreCase))?.Value;

        var userId = context.User.Claims
            .FirstOrDefault(c => c.Type.Contains(Constants.Claims.Id, StringComparison.OrdinalIgnoreCase))?.Value;

        logger.LogInformation("{Date:G}|{id}|{email}|{Url}", DateTime.Now, userId, userEmail, context.Request.GetDisplayUrl());
        // Call the next delegate/middleware in the pipeline.
        await next(context);
    }

    private static void AdoptClientCulture(HttpContext context)
    {
        var cultureQuery = context.Request.Query[Constants.RequestsConstants.Headers.Culture];
        if (string.IsNullOrWhiteSpace(cultureQuery))
        {
            return;
        }

        var culture = new CultureInfo(cultureQuery);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }
}