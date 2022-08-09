// <copyright file="ExceptionFilter.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.Serialization;
using Uow.Domain.Exceptions;

namespace Uow.API.Filters;

internal sealed class ExceptionFilter : IAsyncExceptionFilter
{
    private static readonly Dictionary<int, ProblemDetails> ProblemDetailsDictionary = new()
    {
        {
            StatusCodes.Status404NotFound,
            new ProblemDetails
            {
                Title = "Not found",
                Status = StatusCodes.Status404NotFound
            }
        },
        {
            StatusCodes.Status500InternalServerError,
            new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError
            }
        },
        {
            StatusCodes.Status400BadRequest,
            new ProblemDetails
            {
                Title = "Bad request",
                Status = StatusCodes.Status400BadRequest
            }
        },
        {
            StatusCodes.Status410Gone,
            new ProblemDetails
            {
                Title = "Resource no longer awailable",
                Status = StatusCodes.Status410Gone
            }
        }
    };

    public Task OnExceptionAsync(ExceptionContext context)
    {
        var (httpStatusCode, detail) = context.Exception switch
        {
            EntityNotFoundException => (StatusCodes.Status404NotFound, nameof(EntityNotFoundException)),
            ArgumentException => (StatusCodes.Status400BadRequest, nameof(ArgumentException)),
            InvalidDataContractException => (StatusCodes.Status400BadRequest, nameof(InvalidDataContractException)),
            _ => (StatusCodes.Status500InternalServerError, context.Exception.GetType().Name)
        };

        var problemDetails = new ProblemDetails
        {
            Title = ProblemDetailsDictionary[httpStatusCode].Title,
            Status = ProblemDetailsDictionary[httpStatusCode].Status,
            Detail = detail
        };

        context.Result = new JsonResult(problemDetails)
        {
            StatusCode = httpStatusCode
        };

        return Task.CompletedTask;
    }
}
