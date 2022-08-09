// <copyright file="HttpClientExtensions.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Sdk.SecondaryAdapters.Extensions;

/// <summary>
/// HttpClientExtensions
/// </summary>
internal static class HttpClientExtensions
{
    /// <summary>
    /// Appends authorization header
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">throws if token is null or empty</exception>
    public static HttpClient Authenticate(this HttpClient httpClient, string token)
    {
        if (httpClient.DefaultRequestHeaders.Contains(Constants.HttpConstants.AuthorizationHeader))
        {
            httpClient.DefaultRequestHeaders.Remove(Constants.HttpConstants.AuthorizationHeader);
        }

        httpClient.DefaultRequestHeaders.Add(Constants.HttpConstants.AuthorizationHeader, $"{Constants.HttpConstants.Bearer} {token ?? throw new ArgumentNullException(nameof(token))}");
        return httpClient;
    }
}
