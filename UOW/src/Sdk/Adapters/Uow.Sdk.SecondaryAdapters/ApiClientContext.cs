// <copyright file="ApiClientContext.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.Sdk.SecondaryAdapters.Auth;
using Uow.Sdk.SecondaryPorts;

namespace Uow.Sdk.SecondaryAdapters;

/// <summary>
/// ApiClientContext
/// </summary>
public sealed class ApiClientContext : IApiClientContext
{
    private readonly IHttpClientFactory clientFactory;
    private readonly ITokenRetriever tokenRetriever;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClientContext"/> class.
    /// </summary>
    /// <param name="tokenRetriever"></param>
    /// <param name="clientFactory"></param>
    public ApiClientContext(
        ITokenRetriever tokenRetriever,
        IHttpClientFactory clientFactory)
    {
        this.tokenRetriever = tokenRetriever ?? throw new ArgumentNullException(nameof(tokenRetriever));
        this.clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
    }

    /// <inheritdoc/>
    public string? Token { get; set; }

    /// <inheritdoc/>
    public async Task<string> RetrieveTokenAsync(CancellationToken cancellationToken = default) =>
        await tokenRetriever.GetTokenAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public HttpClient CreateClient() => clientFactory.CreateClient(Constants.InternalConstants.InjectedHttpClientName);
}