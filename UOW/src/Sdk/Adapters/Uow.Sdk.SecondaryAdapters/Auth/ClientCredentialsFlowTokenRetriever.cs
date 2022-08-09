// <copyright file="ClientCredentialsFlowTokenRetriever.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Uow.Sdk.SecondaryAdapters.Models;

namespace Uow.Sdk.SecondaryAdapters.Auth;

/// <summary>
/// Jwt Retriever via client credentials flow
/// </summary>
internal sealed class ClientCredentialsFlowTokenRetriever : ITokenRetriever
{
    private readonly Oauth2Settings settings;
    private readonly IConfidentialClientApplication confidentialClientApp;
    private readonly ILogger<ClientCredentialsFlowTokenRetriever> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientCredentialsFlowTokenRetriever"/> class.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="logger"></param>
    public ClientCredentialsFlowTokenRetriever(
        Oauth2Settings settings,
        ILogger<ClientCredentialsFlowTokenRetriever> logger)
    {
        this.settings = settings;
        this.logger = logger;

        this.confidentialClientApp = ConfidentialClientApplicationBuilder
            .Create(settings.ClientId)
            .WithClientSecret(settings.ClientSecret)
            .WithAuthority(new Uri(settings.AuthUrl))
            .Build();
    }

    /// <inheritdoc/>
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        logger.LogTrace("Authenticate via client credentials flow");

        var authenticationResult = await confidentialClientApp.AcquireTokenForClient(settings.FinalScope)
            .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        return authenticationResult.AccessToken;
    }
}
