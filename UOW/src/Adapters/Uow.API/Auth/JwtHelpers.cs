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
        if (model == null)
        {
            throw new ArgumentException(nameof(model));
        }

        // Get secret key
        var key = GetKey(jwtSettings);
        var expireTime = DateTime.UtcNow.AddDays(1);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);

        var jwToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: GetClaims(model, out var id),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: signingCredentials);

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var userToken = new UserTokens
            {
                Validity = expireTime.TimeOfDay,
                Token = jwtSecurityTokenHandler.WriteToken(jwToken),
                UserName = model.UserName,
                Id = model.Id,
                GuidId = id
            };

        return userToken;
    }

    public static string GenerateToken(UserTokens user, JwtSettings jwtSettings)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = GetKey(jwtSettings);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = signingCredentials,
            Issuer = jwtSettings.ValidIssuer,
            Audience = jwtSettings.ValidAudience,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static JwtSecurityToken? ValidateToken(string? token, JwtSettings jwtSettings)
    {
        if (token == null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = GetKey(jwtSettings);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // return user id from JWT token if validation successful
            return jwtToken;
        }
        catch
        {
            return null;
        }
    }

    private static byte[] GetKey(JwtSettings jwtSettings) =>
        Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

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