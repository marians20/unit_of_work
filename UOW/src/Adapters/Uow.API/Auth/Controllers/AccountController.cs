// <copyright file="AccountController.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uow.API.Auth.Models;

namespace Uow.API.Auth.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private static readonly IEnumerable<User> Logins = new List<User>() {
        new() {
            Id = Guid.NewGuid(),
            EmailId = "adminakp@gmail.com",
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

    public AccountController(JwtSettings jwtSettings) => this.jwtSettings = jwtSettings;


    [HttpPost]
    public IActionResult GetToken(UserLogin userLogin)
    {
        if (!Logins.Any(UserByNameAndPasswordPredicate(userLogin.UserName, userLogin.Password)))
        {
            return BadRequest($"wrong user");
        }

        var user = Logins.First(UserByNameAndPasswordPredicate(userLogin.UserName, userLogin.Password));
        var userTokens = JwtHelpers.GenTokenKey(new UserTokens()
        {
            EmailId = user.EmailId,
            GuidId = Guid.NewGuid(),
            UserName = user.UserName,
            Id = user.Id,
        }, jwtSettings);

        return Ok(userTokens);
    }
    /// <summary>
    /// Get List of UserAccounts
    /// </summary>
    /// <returns>List Of UserAccounts</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetList() => Ok(Logins);

    private static Func<User, bool> UserByNameAndPasswordPredicate(string userName, string password) => x
        => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
            && x.Password.Equals(password, StringComparison.InvariantCulture);
}