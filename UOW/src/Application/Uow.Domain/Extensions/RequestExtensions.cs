// <copyright file="RequestExtensions.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Uow.Domain.Extensions;

public static class RequestExtensions
{
    public static string GetRequestId(this HttpRequest request) =>
        request.Headers[Constants.RequestsConstants.Headers.XRequestId];

    public static string CreateRequestIdIfNotExists(this HttpRequest request)
    {
        if (request.Headers.ContainsKey(Constants.RequestsConstants.Headers.XRequestId))
        {
            return request.Headers[Constants.RequestsConstants.Headers.XRequestId].First();
        }

        var requestId = Guid.NewGuid().ToString();
        request.Headers.Add(Constants.RequestsConstants.Headers.XRequestId, new StringValues(requestId));
        return requestId;

    }
}