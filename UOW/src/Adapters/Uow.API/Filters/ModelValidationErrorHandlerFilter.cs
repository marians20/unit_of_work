// <copyright file="ModelValidationErrorHandlerFilter.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Uow.API.Filters;

public class ModelValidationErrorHandlerFilter : IAsyncActionFilter
{
    /// <inheritdoc/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            await next().ConfigureAwait(false);
            return;
        }

        context.Result = new BadRequestObjectResult(context.ModelState.Keys.Select(k => new { Key = k, Value = context.ModelState[k] }));
    }
}
