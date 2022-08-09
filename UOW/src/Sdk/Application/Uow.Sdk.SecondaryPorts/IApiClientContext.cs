// <copyright file="IApiClientContext.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Sdk.SecondaryPorts;

/// <summary>
/// IApiClientContext
/// </summary>
public interface IApiClientContext
{
    /// <summary>
    /// JWT
    /// </summary>
    string? Token { get; set; }

    /// <summary>
    /// Creates client
    /// </summary>
    /// <returns></returns>
    HttpClient CreateClient();

    /// <summary>
    /// Retrieves a jwt
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> RetrieveTokenAsync(CancellationToken cancellationToken = default);
}