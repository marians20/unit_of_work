// <copyright file="ApiClient.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using Uow.Sdk.SecondaryAdapters.Extensions;
using Uow.Sdk.SecondaryPorts;

namespace Uow.Sdk.SecondaryAdapters;

/// <summary>
/// API Client
/// </summary>
public sealed class ApiClient : IApiClient
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new ()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly HttpClient httpClient;
    private readonly IApiClientContext context;
    private readonly ILogger<ApiClient> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClient"/> class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public ApiClient(IApiClientContext context, ILogger<ApiClient> logger)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        httpClient = this.context.CreateClient() ?? throw new NullReferenceException(nameof(httpClient));

        if (!string.IsNullOrEmpty(this.context.Token))
        {
            httpClient.Authenticate(this.context.Token);
        }
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task<TResponse?> Get<TResponse>(string url, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"GET|{url}");
        var response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
        EnsureResponseIsSuccess(response);
        return await ReadAsAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse?> Post<TResponse>(string url, object payload, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"POST|{url}");
        var response = await httpClient.PostAsync(url, CreateContent(payload), cancellationToken).ConfigureAwait(false);
        EnsureResponseIsSuccess(response);
        return await ReadAsAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse?> Put<TResponse>(string url, object payload, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"PUT|{url}");
        var response = await httpClient.PutAsync(url, CreateContent(payload), cancellationToken).ConfigureAwait(false);
        EnsureResponseIsSuccess(response);
        return await ReadAsAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<TResponse?> Delete<TResponse>(string url, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"DELETE|{url}");
        var response = await httpClient.DeleteAsync(url, cancellationToken).ConfigureAwait(false);
        EnsureResponseIsSuccess(response);
        return await ReadAsAsync<TResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task Authenticate()
    {
        logger.LogInformation($"LOGIN");
        logger.LogInformation($"Get JWT");
        var token = await context.RetrieveTokenAsync().ConfigureAwait(false);
        context.Token = token;
        httpClient.Authenticate(context.Token);
    }

    /// <inheritdoc/>
    public async Task Authenticate(string token)
    {
        logger.LogInformation($"LOGIN");
        context.Token = token;
        httpClient.Authenticate(context.Token);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public void Logout()
    {
        logger.LogInformation($"LOGOUT");
        context.Token = string.Empty;
        httpClient.DefaultRequestHeaders.Remove(Constants.HttpConstants.AuthorizationHeader);
    }

    /// <summary>
    /// If response status code is not success throws <see cref="HttpRequestException"/> exception
    /// </summary>
    /// <param name="response"></param>
    /// <exception cref="HttpRequestException">exception thrown when response has no success status code</exception>
    private static void EnsureResponseIsSuccess(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"{response.StatusCode}");
        }
    }

    /// <summary>
    /// reads body and deserialize it as TResponse/>
    /// </summary>
    /// <typeparam name="TResponse">expected body type</typeparam>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<TResponse?> ReadAsAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var body = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<TResponse?>(body, JsonSerializerOptions, cancellationToken);
    }

    /// <summary>
    /// Creates JsonContent from object
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    private static HttpContent CreateContent(object payload) => JsonContent.Create(payload);
}
