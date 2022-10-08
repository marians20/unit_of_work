// <copyright file="AuthService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.API.Auth.Models;
using Uow.API.Auth.Specifications;

namespace Uow.API.Auth.Services;

public sealed class AuthService : IAuthService
{
    private static readonly IEnumerable<User> Logins = new List<User>() {
        new() {
            Id = Guid.NewGuid(),
            EmailId = "admin@gmail.com",
            UserName = "Admin",
            Password = "Admin",
        },
        new() {
            Id = Guid.NewGuid(),
            EmailId = "adminakp@gmail.com",
            UserName = "User1",
            Password = "Admin",
        }
    };

    private readonly JwtSettings jwtSettings;

    public AuthService(JwtSettings jwtSettings) => this.jwtSettings = jwtSettings;

    public UserTokens? GetToken(UserLogin userLogin)
    {
        var filter = new UserByNameAndPassword(userLogin.UserName, userLogin.Password);
        if (!Logins.Any(filter))
        {
            return null;
        }

        var user = Logins.First(filter);
        var userTokens = JwtHelpers.GenTokenKey(new UserTokens()
        {
            EmailId = user.EmailId,
            GuidId = Guid.NewGuid(),
            UserName = user.UserName,
            Id = user.Id,
        }, jwtSettings);

        return userTokens;
    }
}
