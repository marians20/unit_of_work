// <copyright file="UserResolverService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Uow.API.Auth;
using Uow.API.Auth.Models;

namespace Uow.API.Services;

public sealed class UserResolverService : IUserResolverService
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly JwtSettings jwtSettings;

    public UserResolverService(IHttpContextAccessor httpContextAccessor, JwtSettings jwtSettings)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.jwtSettings = jwtSettings;
    }

    public string? GetUserId()
    {
        var token = GetJwtToken();

        return token?.Claims?.FirstOrDefault(cl => cl.Type == ClaimTypes.Sid)?.Value;
    }

    private JwtSecurityToken? GetJwtToken()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            return null;
        }

        var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers.Authorization
            .ToString().Replace("Bearer ", string.Empty, StringComparison.InvariantCulture);

        return string.IsNullOrEmpty(authorizationHeader.Trim())
            ? null
            : JwtHelpers.ValidateToken(authorizationHeader, jwtSettings);
    }
}
