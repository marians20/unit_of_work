// <copyright file="OnBehalfOfFlowTokenRetriever.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Uow.Sdk.SecondaryAdapters.Models;

namespace Uow.Sdk.SecondaryAdapters.Auth;

/// <summary>
/// OnBehalfOfFlowTokenRetriever
/// </summary>
internal sealed class OnBehalfOfFlowTokenRetriever : ITokenRetriever
{
    private readonly Oauth2Settings settings;
    private readonly IPublicClientApplication publicClientApplication;
    private readonly ILogger<OnBehalfOfFlowTokenRetriever> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OnBehalfOfFlowTokenRetriever"/> class.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="logger"></param>
    public OnBehalfOfFlowTokenRetriever(Oauth2Settings settings, ILogger<OnBehalfOfFlowTokenRetriever> logger)
    {
        this.settings = settings;
        this.logger = logger;

        publicClientApplication = PublicClientApplicationBuilder.Create(settings.ClientId)
            .WithAuthority(settings.Authority)
            .WithDefaultRedirectUri()
            .Build();
    }

    /// <inheritdoc/>
    public Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
