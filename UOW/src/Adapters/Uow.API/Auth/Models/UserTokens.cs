// <copyright file="UserTokens.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.API.Auth.Models;

public class UserTokens
{
    public string Token { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public TimeSpan Validity { get; set; }

    public string RefreshToken { get; set; } = default!;

    public Guid Id { get; set; }

    public string EmailId { get; set; } = default!;

    public Guid GuidId { get; set; }

    public DateTime ExpiredTime { get; set; }
}