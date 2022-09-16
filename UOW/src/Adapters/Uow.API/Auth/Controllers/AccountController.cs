// <copyright file="AccountController.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Uow.API.Auth.Models;
using Uow.API.Auth.Services;

namespace Uow.API.Auth.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService authService;

    public AccountController(IAuthService authService) => this.authService = authService;


    [HttpPost]
    public IActionResult Login(UserLogin userLogin)
    {
        var userTokens = authService.GetToken(userLogin);
        if (userTokens == null)
        {
            return Unauthorized();
        }

        return Ok(userTokens);
    }
}