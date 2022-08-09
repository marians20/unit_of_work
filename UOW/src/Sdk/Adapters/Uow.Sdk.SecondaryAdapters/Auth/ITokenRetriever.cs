// <copyright file="ITokenRetriever.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Sdk.SecondaryAdapters.Auth;

/// <summary>
/// ITokenRetriever
/// </summary>
public interface ITokenRetriever
{
    /// <summary>
    /// GetTokenAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
}