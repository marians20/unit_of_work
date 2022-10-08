// <copyright file="JwtHelpers.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Uow.API.Auth.Models;

namespace Uow.API.Auth;

public static class JwtHelpers
{
    /// <summary>
    /// Generates a UserTokens object
    /// </summary>
    /// <param name="model"></param>
    /// <param name="jwtSettings"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
    {
        var userToken = new UserTokens();
        if (model == null)
        {
            throw new ArgumentException(nameof(model));
        }

        // Get secret key
        var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
        var expireTime = DateTime.UtcNow.AddDays(1);
        userToken.Validity = expireTime.TimeOfDay;
        var jwToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: GetClaims(model, out var id),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
        userToken.UserName = model.UserName;
        userToken.Id = model.Id;
        userToken.GuidId = id;
        return userToken;
    }

    /// <summary>
    /// Reads a JwtSecurityToken from a JWT string with or without validation
    /// </summary>
    /// <param name="token"></param>
    /// <param name="settings"></param>
    /// <param name="validate"></param>
    /// <returns></returns>
    public static JwtSecurityToken GetJwtSecurityToken(string token, JwtSettings settings, bool validate = false)
    {
        if (!validate)
        {
            return new JwtSecurityToken(token);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(settings.IssuerSigningKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuer = settings.ValidIssuer,
            ValidateIssuer = !string.IsNullOrEmpty(settings.ValidIssuer),
            ValidAudience = settings.ValidAudience,
            ValidateAudience = !string.IsNullOrEmpty(settings.ValidAudience),

            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero
        };

        tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

        return (JwtSecurityToken)validatedToken;
    }

    private static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid id) =>
        new Claim[] {
            new("Id", userAccounts.Id.ToString()),
            new(ClaimTypes.Name, userAccounts.UserName),
            new(ClaimTypes.Email, userAccounts.EmailId),
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        };

    private static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid id)
    {
        id = Guid.NewGuid();
        return GetClaims(userAccounts, id);
    }
}