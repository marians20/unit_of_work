// <copyright file="IApiClient.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Sdk.SecondaryPorts;

/// <summary>
/// Api Client
/// </summary>
public interface IApiClient
{
    /// <summary>
    /// Authenticates by retrieving token
    /// </summary>
    /// <returns></returns>
    Task Authenticate();

    /// <summary>
    /// Authenticates using the provided token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task Authenticate(string token);

    /// <summary>
    /// Clears the existing token
    /// </summary>
    void Logout();

    /// <summary>
    /// Sends Get requests and deserialize response as TResponse
    /// </summary>
    /// <typeparam name="TResponse">type of expected response</typeparam>
    /// <param name="url"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> Get<TResponse>(string url, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a Post request and deserializes response as TResponse
    /// </summary>
    /// <typeparam name="TResponse">type of expected response</typeparam>
    /// <param name="url"></param>
    /// <param name="payload"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> Post<TResponse>(string url, object payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a Put request and deserializes response as TResponse
    /// </summary>
    /// <typeparam name="TResponse">type of expected response</typeparam>
    /// <param name="url"></param>
    /// <param name="payload"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> Put<TResponse>(string url, object payload, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a Delete request and deserializes response as TResponse
    /// </summary>
    /// <typeparam name="TResponse">type of expected response</typeparam>
    /// <param name="url"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> Delete<TResponse>(string url, CancellationToken cancellationToken = default);
}