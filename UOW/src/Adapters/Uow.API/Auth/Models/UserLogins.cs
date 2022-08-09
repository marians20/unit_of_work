// <copyright file="UserLogin.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using System.ComponentModel.DataAnnotations;
namespace Uow.API.Auth.Models;

public class UserLogin
{
    [Required]
    public string UserName { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;

    public UserLogin() { }
}