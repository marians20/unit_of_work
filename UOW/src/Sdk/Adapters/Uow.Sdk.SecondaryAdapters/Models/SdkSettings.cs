// <copyright file="SdkSettings.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Sdk.SecondaryAdapters.Models;

/// <summary>
/// SDK Settings
/// </summary>
public sealed class SdkSettings
{
    /// <summary>
    /// BaseUrl
    /// </summary>
    public string BaseUrl { get; set; } = default!;
}
